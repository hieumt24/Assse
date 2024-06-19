using AssetManagement.Application.Filter;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Assets.Responses;
using AssetManagement.Application.Models.Filters;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces.Services
{
    public interface IAssetServiceAsync
    {
        Task<Response<AssetDto>> AddAssetAsync(AddAssetRequestDto request);

        Task<PagedResponse<List<AssetResponseDto>>> GetAllAseets(PaginationFilter pagination, string? search, Guid? categoryId, AssetStateType? assetStateType, EnumLocation enumLocation, string? orderBy, bool? isDescending, string? route);
    }
}