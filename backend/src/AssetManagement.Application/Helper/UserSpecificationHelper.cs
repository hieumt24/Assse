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
        public static ISpecification<User> CreateSpecification(string? search, EnumLocation? adminLocation, RoleType? roleType, string? orderBy, bool? isDescending)
        {
            Expression<Func<User, bool>> criteria = user => !user.IsDeleted && !user.IsDisable;

            if (!string.IsNullOrEmpty(search))
            {
                criteria = user => user.FirstName.Contains(search) || user.LastName.Contains(search) || user.Username.Contains(search) || user.StaffCode.Contains(search);
            }

            if (adminLocation.HasValue)
            {
                Expression<Func<User, bool>> locationCriteria = user => user.Location == adminLocation.Value;
                criteria = criteria.And(locationCriteria);
            }
            if (roleType.HasValue)
            {
                Expression<Func<User, bool>> roleCriteria = user => user.Role == roleType.Value;
                criteria = criteria.And(roleCriteria);
            }
            var spec = new UserSpecification(criteria);

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (isDescending.HasValue)
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

            return spec;
        }

        public static ISpecification<User> CreateSpecificationPagination(ISpecification<User> userQuery, PaginationFilter filter)
        {
            var useragination = new UserSpecification(userQuery.Criteria);
            useragination.ApplyPaging(filter.PageSize * (filter.PageNumber - 1), filter.PageSize);
            return useragination;
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

        public static ISpecification<User> GetUserByStaffCode(string staffCode)
        {
            return new UserSpecification(user => user.StaffCode == staffCode && !user.IsDeleted);
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