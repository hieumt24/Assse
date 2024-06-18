using AssetManagement.Application.Common;
using AssetManagement.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Interfaces
{
    public interface ICategoryRepositoriesAsync : IBaseRepositoryAsync<Category>
    {
      Task<bool> IsCategoryNameDuplicateAsync(string categoryName);

      Task<bool> IsPrefixDuplicateAsync(string prefix);

      Task<List<Category>> ListAllActiveAsync();

    }
}
