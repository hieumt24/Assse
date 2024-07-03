using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Reports.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Services
{
    public class ReportService : IReportServices
    {
        private readonly ICategoryRepositoriesAsync _categoryRepositoriesAsync;
        private readonly IAssetRepositoriesAsync _assetRepositoriesAsync;
        private readonly IUriService _uriService;

        public ReportService(ICategoryRepositoriesAsync categoryRepositoriesAsync, IAssetRepositoriesAsync assetRepositoriesAsync, IUriService uriService)
        {
            _categoryRepositoriesAsync = categoryRepositoriesAsync;
            _assetRepositoriesAsync = assetRepositoriesAsync;
            _uriService = uriService;
        }

        public Task<PagedResponse<ReportResponseDto>> FilterReportAsync(PaginationFilter pagination, EnumLocation location, string? orderBy, bool? isDescending, string? route)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<ReportResponseDto>>> GetReportAsync(EnumLocation location, PaginationFilter pagination, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                var reportResponseDtos = new List<ReportResponseDto>();

                var categories = await _categoryRepositoriesAsync.ListAllAsync();

                var asset = AssetSpecificationHelper.GetAllAssets(location);
                var totalAssets = await _assetRepositoriesAsync.ListAsync(asset);

                foreach (var category in categories)
                {
                    var report = new ReportResponseDto
                    {
                        CategoryId = category.Id,
                        CategoryName = category.CategoryName,
                        Total = totalAssets.Count(x => x.CategoryId == category.Id),
                        Assigned = totalAssets.Count(x => x.CategoryId == category.Id && x.State == AssetStateType.Assigned),
                        Available = totalAssets.Count(x => x.CategoryId == category.Id && x.State == AssetStateType.Available),
                        NotAvailable = totalAssets.Count(x => x.CategoryId == category.Id && x.State == AssetStateType.NotAvailable),
                        Waitingforrecycling = totalAssets.Count(x => x.CategoryId == category.Id && x.State == AssetStateType.WaitingForRecycling),
                        Recycled = totalAssets.Count(x => x.CategoryId == category.Id && x.State == AssetStateType.Recycled)
                    };

                    reportResponseDtos.Add(report);
                }
                var reportResponses = ReportHelper.ApplySorting(reportResponseDtos, orderBy, isDescending);
                var totalRecord = reportResponses.Count();

                var pagedResponse = PaginationHelper.CreatePagedReponse(reportResponses, pagination, totalRecord, _uriService, route);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<ReportResponseDto>> { Errors = { ex.Message } };
            }
        }
    }
}