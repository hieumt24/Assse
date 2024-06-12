using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AssetManagement.Application.Services
{
    public class UserServiceAsync : IUserServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IUserRepositoriesAsync _userRepositoriesAsync;
        private readonly IValidator<AddUserRequestDto> _validator;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserServiceAsync(IUserRepositoriesAsync userRepositoriesAsync,
            IMapper mapper,
            IValidator<AddUserRequestDto> validator)
        {
            _mapper = mapper;
            _userRepositoriesAsync = userRepositoriesAsync;
            _validator = validator;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request)
        {
            var validationResult = await _validator.ValidateAsync(request);
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
                user.Username = user.Username + user.StaffCodeId;
                await _userRepositoriesAsync.UpdateAsync(user);

                var userDto = _mapper.Map<UserDto>(user);

                return new Response<UserDto>();
            }
            catch (Exception ex)
            {
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }
    }
}