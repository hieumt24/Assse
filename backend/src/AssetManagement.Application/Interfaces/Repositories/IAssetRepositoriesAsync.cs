using AssetManagement.Application.Common;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Interfaces.Repositories
{
    public interface IAssetRepositoriesAsync : IBaseRepositoryAsync<Asset>
    {
        Task<string> GenerateAssetCodeAsync(Guid CategoryId);

        Task<IQueryable<Asset>> FilterAsset(EnumLocation adminLocation, string? search, Guid? categoryId, ICollection<AssetStateType?>? assetStateType);
    }
}