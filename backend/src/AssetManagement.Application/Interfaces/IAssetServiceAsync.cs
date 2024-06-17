using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Wrappers;


namespace AssetManagement.Application.Interfaces
{
    public interface IAssetServiceAsync
    {
        Task<Response<AssetDto>> AddAssetAsync(AddAssetRequestDto request);
    }
}
