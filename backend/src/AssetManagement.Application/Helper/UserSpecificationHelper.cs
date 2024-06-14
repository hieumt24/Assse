using AssetManagement.Application.Filter;
using AssetManagement.Application.Services;
using AssetManagement.Domain.Common.Specifications;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using System.Linq.Expressions;

namespace AssetManagement.Application.Helper
{
    public static class UserSpecificationHelper
    {
        public static ISpecification<User> CreateSpecification(PaginationFilter filter, string? search, string? orderBy, bool isDescending, EnumLocation? adminLocation)
        {
            Expression<Func<User, bool>> criteria = user => true;

            if (!string.IsNullOrEmpty(search))
            {
                criteria = user => user.FirstName.Contains(search) || user.LastName.Contains(search) || user.Username.Contains(search);
            }

            if (adminLocation.HasValue)
            {
                Expression<Func<User, bool>> locationCriteria = user => user.Location == adminLocation.Value;
                criteria = criteria.And(locationCriteria);
            }

            var spec = new UserSpecification(criteria);

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (isDescending)
                {
                    spec.ApplyOrderByDescending(GetOrderByExpression(orderBy));
                }
                else
                {
                    spec.ApplyOrderBy(GetOrderByExpression(orderBy));
                }
            }
            else
            {
                spec.ApplyOrderByDescending(u => u.CreatedOn);
            }

            spec.ApplyPaging((filter.PageNumber - 1) * filter.PageSize, filter.PageSize);

            return spec;
        }

        private static Expression<Func<User, object>> GetOrderByExpression(string orderBy)
        {
            return orderBy.ToLower() switch
            {
                "firstname" => u => u.FirstName,
                "lastname" => u => u.LastName,
                "username" => u => u.Username,
                "dateofbirth" => u => u.DateOfBirth,
                "joineddate" => u => u.JoinedDate,
                "gender" => u => u.Gender,
                _ => u => u.FirstName,
            };
        }

        public static ISpecification<User> TotalUser()
        {
            return new UserSpecification(user => !user.IsDeleted);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}