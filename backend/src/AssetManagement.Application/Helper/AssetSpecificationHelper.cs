using AssetManagement.Application.Filter;
using AssetManagement.Domain.Common.Specifications;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AssetManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace AssetManagement.Application.Helper
{
    public class AssetSpecificationHelper
    {
        public static ISpecification<Asset> CreateAssetQuery(string? search, Guid? categoryId, AssetStateType? assetStateType, EnumLocation enumLocation, string? orderBy, bool? isDescending)
        {
            Expression<Func<Asset, bool>> criteria = asset => !asset.IsDeleted && asset.AssetLocation == enumLocation;
            if (!string.IsNullOrEmpty(search))
            {
                criteria = asset => asset.AssetCode.ToLower().Contains(search.ToLower()) || asset.AssetName.ToLower().Contains(search.ToLower());
            }
            if (assetStateType.HasValue)
            {
                Expression<Func<Asset, bool>> assetStateCriteria = asset => asset.State == assetStateType.Value;
                criteria = criteria.And(assetStateCriteria);
            }
            if (categoryId.HasValue)
            {
                Expression<Func<Asset, bool>> categoryCriteria = asset => asset.CategoryId == categoryId.Value;
                criteria = criteria.And(categoryCriteria);
            }

            var spec = new AssetSpecification(criteria);
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (isDescending.HasValue && isDescending.Value)
                {
                    spec.ApplyOrderByDescending(GetOrderByExpression(orderBy));
                }
                else
                {
                    spec.ApplyOrderBy(GetOrderByExpression(orderBy));
                }
            }
            return spec;
        }

        public static ISpecification<Asset> CreateAssetPagination(ISpecification<Asset> assetQuery, PaginationFilter filter)
        {
            var assetPagination = new AssetSpecification(assetQuery.Criteria);
            assetPagination.ApplyPaging(filter.PageSize * (filter.PageIndex - 1), filter.PageSize);
            return assetPagination;
        }

        private static Expression<Func<Asset, object>> GetOrderByExpression(string orderBy)
        {
            return orderBy.ToLower() switch
            {
                "assetcode" => u => u.AssetCode,
                "assetname" => u => u.AssetName,
                "installeddate" => u => u.InstalledDate,
                "state" => u => u.State,
                "assetlocation" => u => u.AssetLocation,
                "categoryname" => u => u.Category.CategoryName,
                _ => u => u.AssetName
            };
        }
    }
}