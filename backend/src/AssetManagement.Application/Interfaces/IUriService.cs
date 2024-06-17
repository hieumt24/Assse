using AssetManagement.Application.Filter;

namespace AssetManagement.Application.Interfaces
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}