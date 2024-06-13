using AssetManagement.Application.Filter;
using AssetManagement.Application.Services;
using AssetManagement.Domain.Common.Specifications;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Helper
{
    public static class UserSpecificationHelper
    {
        public static ISpecification<User> CreateSpecification(PaginationFilter filter, string? search, string? orderBy, bool isDescending, EnumLocation? adminLocation)
        {
            return new UserSpecification<User>(
                criteria: u => (string.IsNullOrEmpty(search) || u.Username.Contains(search) || u.StaffCode.Contains(search)) && u.Location == adminLocation,
                orderBy: !string.IsNullOrEmpty(orderBy) && !isDescending ? GetOrderByExpression(orderBy) : null,
                orderByDescending: !string.IsNullOrEmpty(orderBy) && isDescending ? GetOrderByExpression(orderBy) : null,
                skip: (filter.PageNumber - 1) * filter.PageSize,
                take: filter.PageSize
            );
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

        //Count User
        public static ISpecification<User> TotalUser()
        {
            return new UserSpecification<User>(
                               criteria: u => u.IsDeleted == false
                                            );
        }
    }
}