using AssetManagement.Domain.Common.Specifications;
using System.Linq.Expressions;

namespace AssetManagement.Application.Services
{
    public class UserSpecification<T> : ISpecification<T>
    {
        public UserSpecification(
            Expression<Func<T, bool>> criteria = null,
            Expression<Func<T, object>> orderBy = null,
            Expression<Func<T, object>> orderByDescending = null,
            int skip = 0,
            int take = 0)
        {
            Criteria = criteria;
            OrderBy = orderBy;
            OrderByDescending = orderByDescending;
            Skip = skip;
            Take = take;
            IsPagingEnabled = take > 0;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; }
        public Expression<Func<T, object>> OrderByDescending { get; }
        public Expression<Func<T, object>> GroupBy { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagingEnabled { get; }
    }
}




