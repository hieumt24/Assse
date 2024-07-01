using AssetManagement.Application.Filter;
using AssetManagement.Application.Models.DTOs.Reports.Responses;
using AssetManagement.Application.Wrappers;

namespace AssetManagement.Application.Interfaces.Services
{
    public interface IReportServices
    {
        Task<PagedResponse<ReportResponseDto>> FilterReportAsync(PaginationFilter pagination, string? orderBy, bool? isDescending, string? route);
    }
}