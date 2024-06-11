using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AutoMapper;

namespace AssetManagement.Application.Services
{
    public class UserServiceAsync : IUserServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IUserRepositoriesAsync _userRepositoriesAsync;

        public UserServiceAsync(IUserRepositoriesAsync userRepositoriesAsync, IMapper mapper)
        {
            _mapper = mapper;
            _userRepositoriesAsync = userRepositoriesAsync;
        }

        public async Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request)
        {
            try
            {
                //Map request to Domain Entity
                var userDomain = _mapper.Map<User>(request);

                //set prop
                userDomain.Username = _userRepositoriesAsync.GenerateUsername(userDomain.FirstName, userDomain.LastName);
                userDomain.Password = _userRepositoriesAsync.GeneratePassword(userDomain.Username, userDomain.DateOfBirth);

                userDomain.IsDeleted = false;
                userDomain.CreatedOn = DateTime.Now;

                //Add user to database
                var user = await _userRepositoriesAsync.AddAsync(userDomain);

                return new Response<UserDto>();
            }
            catch (Exception ex)
            {
                //return error message
                throw new Exception(ex.Message);
            }
        }
    }
}