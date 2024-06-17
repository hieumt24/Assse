﻿using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Entites;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Infrastructure.Repositories
{
    public class AssetRepository : BaseRepositoryAsync<Asset>, IAssetRepositoriesAsync
    {
        public AssetRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<string> GenerateAssetCodeAsync(Guid CategoryId)
        {
            // Get the category to access its name
            var category = await _dbContext.Categories.FindAsync(CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            string prefix = new string(category.CategoryName.ToUpper().Take(2).ToArray());

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
