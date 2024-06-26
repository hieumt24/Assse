using AssetManagement.Application.Common;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces.Repositories
{
    public interface IAssetRepositoriesAsync : IBaseRepositoryAsync<Asset>
    {
        Task<string> GenerateAssetCodeAsync(Guid CategoryId);

        Task<IQueryable<Asset>> FilterAsset(EnumLocation adminLocation, string? search, Guid? categoryId, ICollection<AssetStateType?>? assetStateType);

        Task<Asset> FindExitingCategory(Guid categoryId);
    }
}