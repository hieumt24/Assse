using System.Linq.Expressions;

namespace AssetManagement.Domain.Common.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        int Take { get; set; }
        int Skip { get; set; }
        bool IsPagingEnabled { get; }
    }
}