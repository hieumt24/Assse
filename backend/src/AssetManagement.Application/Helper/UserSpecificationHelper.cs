using AssetManagement.Application.Filter;
using AssetManagement.Domain.Common.Specifications;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Specifications;
using System.Linq.Expressions;

namespace AssetManagement.Application.Helper
{
    public static class UserSpecificationHelper
    {
        public static ISpecification<User> CreateSpecification(PaginationFilter pagination, string? orderBy, bool? isDescending)
        {
            Expression<Func<User, bool>> criteria = user => true;

            var spec = new UserSpecification(criteria);

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
            else
            {
                spec.ApplyOrderBy(u => u.FirstName);
                spec.ApplyOrderBy(u => u.StaffCode);
                spec.ApplyOrderBy(u => u.Username);
                spec.ApplyOrderBy(u => u.JoinedDate);
                spec.ApplyOrderBy(u => u.Role);
            }
            spec.ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);

            return spec;
        }

        public static ISpecification<User> CreateSpecificationPagination(ISpecification<User> userQuery, PaginationFilter filter)
        {
            var useragination = new UserSpecification(userQuery.Criteria);
            useragination.ApplyPaging(filter.PageSize * (filter.PageIndex - 1), filter.PageSize);
            return useragination;
        }

        private static Expression<Func<User, object>> GetOrderByExpression(string orderBy)
        {
            switch (orderBy.ToLower())
            {
                case "firstname":
                    return u => u.FirstName;

                case "lastname":
                    return u => u.LastName;

                case "username":
                    return u => u.Username;

                case "role":
                    return u => u.Role;

                case "dateofbirth":
                    return u => u.DateOfBirth;

                case "joineddate":
                    return u => u.JoinedDate;

                case "gender":
                    return u => u.Gender;

                case "staffcode":
                    return u => u.StaffCode;

                case "lastmodifiedon":
                    return u => u.LastModifiedOn;

                case "createdon":
                    return u => u.CreatedOn;

                default:
                    return u => u.FirstName;
            }
        }

        public static ISpecification<User> GetUserByStaffCode(string staffCode)
        {
            return new UserSpecification(user => user.StaffCode == staffCode);
        }
    }
}