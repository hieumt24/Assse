using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class AssetRepository : BaseRepositoryAsync<Asset>, IAssetRepositoriesAsync
    {
        public AssetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Asset>> FilterAsset(EnumLocation adminLocation, string? search, Guid? categoryId, ICollection<AssetStateType?>? assetStateType)
        {
            //check asset by adminLocation

            var query = _dbContext.Assets.Where(x => x.AssetLocation == adminLocation);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.AssetName.ToLower().Contains(search.ToLower()) || x.AssetCode.ToLower().Contains(search.ToLower()));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            if (assetStateType != null && assetStateType.Count > 0)
            {
                query = query.Where(x => assetStateType.Contains(x.State));
            }
            else
            {
                query = query.Where(x => x.State == AssetStateType.Available
                                                       || x.State == AssetStateType.NotAvailable
                                                       || x.State == AssetStateType.Assigned
                                                       || x.State == AssetStateType.WaitingForRecycling
                                                       || x.State == AssetStateType.Recycled
                                                       || x.State == AssetStateType.Recycled);
            }
            return query;
        }

        public async Task<Asset> FindExitingCategory(Guid categoryId)
        {
            return await _dbContext.Set<Asset>()
                .FirstOrDefaultAsync(asset => asset.CategoryId == categoryId);
        }

        public async Task<string> GenerateAssetCodeAsync(Guid CategoryId)
        {
            // Get the category to access its prefix
            var category = await _dbContext.Categories.FindAsync(CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            // Use the prefix from the category
            string prefix = category.Prefix.ToUpper();

            // Get the last asset code for this category
            var lastAssetCode = await _dbContext.Assets
                .Where(a => a.CategoryId == CategoryId && a.AssetCode.StartsWith(prefix))
                .OrderByDescending(a => a.AssetCode)
                .Select(a => a.AssetCode)
                .FirstOrDefaultAsync();

            // Generate the new asset code
            int newNumber = 1;
            if (lastAssetCode != null)
            {
                int lastNumber = int.Parse(lastAssetCode.Substring(prefix.Length));
                newNumber = lastNumber + 1;
            }

            return $"{prefix}{newNumber:D6}";
        }
    }
}