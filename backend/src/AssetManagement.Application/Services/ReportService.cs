using AssetManagement.Application.Filter;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Reports.Responses;
using AssetManagement.Application.Wrappers;

namespace AssetManagement.Application.Services
{
    public class ReportService : IReportServices
    {
        public Task<PagedResponse<ReportResponseDto>> FilterReportAsync(PaginationFilter pagination, string? orderBy, bool? isDescending, string? route)
        {
            throw new NotImplementedException();
        }
    }
}