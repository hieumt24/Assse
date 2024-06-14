using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Wrappers;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Domain.Enums;
using AssetManagement.Application.Filter;

namespace AssetManagement.Application.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request);

        Task<PagedResponse<List<UserResponseDto>>> GetAllUsersAsync(PaginationFilter filter, string? search, string? orderBy, bool isDescending, EnumLocation? adminLocation, string route);

        Task<Response<UserResponseDto>> GetUserByIdAsync(Guid userId);

        Task<Response<UserDto>> EditUserAsync(EditUserRequestDto request);
    }
}