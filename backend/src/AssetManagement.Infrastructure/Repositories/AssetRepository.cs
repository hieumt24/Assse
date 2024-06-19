using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
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

        public IQueryable<Asset> Query()
        {
            return _dbContext.Assets;
        }
    }
}