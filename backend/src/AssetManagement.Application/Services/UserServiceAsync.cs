using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

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

        public UserServiceAsync(IUserRepositoriesAsync userRepositoriesAsync,
            IMapper mapper,
            IValidator<AddUserRequestDto> addUserValidator,
            IValidator<EditUserRequestDto> editUserValidator,
            IUriService uriService
        )
        {
            _mapper = mapper;
            _userRepositoriesAsync = userRepositoriesAsync;
            _addUserValidator = addUserValidator;
            _editUserValidator = editUserValidator;
            _passwordHasher = new PasswordHasher<User>();
            _uriService = uriService;
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

        public async Task<PagedResponse<List<UserResponseDto>>> GetAllUsersAsync(PaginationFilter filter, string? search, string? orderBy, bool isDescending, EnumLocation? adminLocation, string route)
        {
            try
            {
                var totalRecords = await _userRepositoriesAsync.CountAsync(UserSpecificationHelper.TotalUser());
                var specification = UserSpecificationHelper.CreateSpecification(filter, search, orderBy, isDescending, adminLocation);
                var users = await _userRepositoriesAsync.ListAsync(specification);
                var userDtos = _mapper.Map<List<UserResponseDto>>(users);

                var pagedResponse = PaginationHelper.CreatePagedReponse(userDtos, filter, totalRecords, _uriService, route);
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

                user.IsDisable = !user.IsDisable;
                await _userRepositoriesAsync.UpdateAsync(user);
                if (user.IsDisable == true)
                {
                    return new Response<UserDto> { Succeeded = true, Message = "User disabled successfully" };
                }
                else
                {
                    return new Response<UserDto> { Succeeded = true, Message = "User enable successfully" };

                }

            }
            catch (Exception ex)
            {
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

    }
}