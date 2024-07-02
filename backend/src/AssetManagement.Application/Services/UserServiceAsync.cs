using AssetManagement.Application.Common;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Application.Services
{
    public class UserServiceAsync : IUserServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IUserRepositoriesAsync _userRepositoriesAsync;
        private readonly IValidator<AddUserRequestDto> _addUserValidator;
        private readonly IValidator<EditUserRequestDto> _editUserValidator;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IUriService _uriService;
        private readonly IAssignmentRepositoriesAsync _assignmentRepositoriesAsync;

        public UserServiceAsync(IUserRepositoriesAsync userRepositoriesAsync,
            IMapper mapper,
            IValidator<AddUserRequestDto> addUserValidator,
            IValidator<EditUserRequestDto> editUserValidator,
            IUriService uriService,
            IAssignmentRepositoriesAsync assignmentRepositoriesAsync
        )
        {
            _mapper = mapper;
            _userRepositoriesAsync = userRepositoriesAsync;
            _addUserValidator = addUserValidator;
            _editUserValidator = editUserValidator;
            _passwordHasher = new PasswordHasher<User>();
            _uriService = uriService;
            _assignmentRepositoriesAsync = assignmentRepositoriesAsync;
        }

        public async Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request)
        {
            var validationResult = await _addUserValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<UserDto> { Succeeded = false, Errors = errors };
            }

            try
            {
                var userDomain = _mapper.Map<User>(request);

                userDomain.Username = await _userRepositoriesAsync.GenerateUsername(userDomain.FirstName, userDomain.LastName);
                userDomain.PasswordHash = _passwordHasher.HashPassword(userDomain, _userRepositoriesAsync.GeneratePassword(userDomain.Username, userDomain.DateOfBirth));
                userDomain.IsDeleted = false;
                userDomain.CreatedOn = DateTime.Now;

                var user = await _userRepositoriesAsync.AddAsync(userDomain);

                var userDto = _mapper.Map<UserDto>(user);

                return new Response<UserDto>();
            }
            catch (Exception ex)
            {
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<UserResponseDto>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _userRepositoriesAsync.GetByIdAsync(userId);
                if (user == null)
                {
                    return new Response<UserResponseDto>("User not found");
                }

                var userDto = _mapper.Map<UserResponseDto>(user);
                return new Response<UserResponseDto> { Data = userDto, Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response<UserResponseDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<PagedResponse<List<UserResponseDto>>> GetAllUsersAsync(PaginationFilter pagination, string? search, EnumLocation adminLocation, RoleType? roleType, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (pagination == null)
                {
                    pagination = new PaginationFilter();
                }
                //queryable user filter condition
                var filterUser = await _userRepositoriesAsync.FilterUserAsync(adminLocation, search, roleType);
                var totalRecords = await filterUser.CountAsync();

                var specUser = UserSpecificationHelper.CreateSpecification(pagination, orderBy, isDescending);

                var users = await SpecificationEvaluator<User>.GetQuery(filterUser, specUser).ToListAsync();

                var userDtos = _mapper.Map<List<UserResponseDto>>(users);

                var pagedResponse = PaginationHelper.CreatePagedReponse(userDtos, pagination, totalRecords, _uriService, route);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<UserResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<UserDto>> EditUserAsync(EditUserRequestDto request)
        {
            try
            {
                var validationResult = await _editUserValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return new Response<UserDto> { Succeeded = false, Errors = errors };
                }

                var existingUser = await _userRepositoriesAsync.GetByIdAsync(request.UserId);
                if (existingUser == null)
                {
                    return new Response<UserDto>("User not found");
                }

                existingUser.DateOfBirth = request.DateOfBirth;
                existingUser.Gender = request.Gender;
                existingUser.JoinedDate = request.JoinedDate;
                existingUser.Role = request.Role;
                existingUser.LastModifiedOn = DateTime.Now;

                await _userRepositoriesAsync.UpdateAsync(existingUser);

                var updatedUserDto = _mapper.Map<UserDto>(existingUser);
                return new Response<UserDto> { Succeeded = true, Data = updatedUserDto };
            }
            catch (Exception ex)
            {
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<UserDto>> DisableUserAsync(Guid userId)
        {
            try
            {
                var user = await _userRepositoriesAsync.GetByIdAsync(userId);
                if (user == null)
                {
                    return new Response<UserDto> { Succeeded = false, Message = "User not found" };
                }
                //Check user have assignment

                var assignments = await _assignmentRepositoriesAsync.GetAssignmentsByUserId(user.Id);
                if (assignments.Any())
                {
                    return new Response<UserDto> { Succeeded = false, Message = "There are valid assignments belonging to this user. Please close all assignments before disabling user." };
                }

                var disableUser = await _userRepositoriesAsync.DeleteAsync(user.Id);
                if (disableUser == null)
                {
                    return new Response<UserDto> { Succeeded = false, Message = "Disable user failed" };
                }
                return new Response<UserDto> { Succeeded = true, Message = "Disable user successfully" };
            }
            catch (Exception ex)
            {
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<UserDto>> ResetPasswordAsync(Guid userId)
        {
            try
            {
                var existingUser = await _userRepositoriesAsync.GetByIdAsync(userId);
                if (existingUser == null)
                {
                    return new Response<UserDto>("User not found");
                }

                string newPassword = _userRepositoriesAsync.GeneratePassword(existingUser.Username, existingUser.DateOfBirth);
                existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, newPassword);

                await _userRepositoriesAsync.UpdateAsync(existingUser);

                var updatedUserDto = _mapper.Map<UserDto>(existingUser);
                return new Response<UserDto> { Succeeded = true, Message = "Reset password successfully" };
            }
            catch (Exception ex)
            {
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<UserResponseDto>> GetUserByStaffCodeAsync(string staffCode)
        {
            try
            {
                var userWithStaffCode = UserSpecificationHelper.GetUserByStaffCode(staffCode);
                if (userWithStaffCode == null)
                {
                    return new Response<UserResponseDto>("User not found");
                }
                var user = await _userRepositoriesAsync.FirstOrDefaultAsync(userWithStaffCode);
                var userDto = _mapper.Map<UserResponseDto>(user);
                return new Response<UserResponseDto> { Succeeded = true, Data = userDto };
            }
            catch (Exception ex)
            {
                return new Response<UserResponseDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }
    }
}