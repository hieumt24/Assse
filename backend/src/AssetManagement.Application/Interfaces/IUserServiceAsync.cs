using AssetManagement.Application.Filter;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request);

        Task<PagedResponse<List<UserResponseDto>>> GetAllUsersAsync(PaginationFilter filter, string? search, string? orderBy, bool isDescending, EnumLocation? adminLocation, string route);

        Task<Response<UserResponseDto>> GetUserByIdAsync(Guid userId);

        Task<Response<UserDto>> EditUserAsync(EditUserRequestDto request);

        Task<Response<UserDto>> DisableUserAsync(Guid userId);

    }
}