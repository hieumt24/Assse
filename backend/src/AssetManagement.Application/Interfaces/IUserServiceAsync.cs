using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Wrappers;

namespace AssetManagement.Application.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request);
    }
}