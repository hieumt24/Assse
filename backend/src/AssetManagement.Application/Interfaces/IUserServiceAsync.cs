using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Wrappers;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request);
        Task<Response<List<UserResponseDto>>> GetAllUsersAsync(string? search, string? orderBy, bool isDescending, int skip, int take, EnumLocation? adminLocation);
        Task<Response<UserResponseDto>> GetUserByIdAsync(Guid userId);
    }
}