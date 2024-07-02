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

        public ReportService(ICategoryRepositoriesAsync categoryRepositoriesAsync, IAssetRepositoriesAsync assetRepositoriesAsync)
        {
            _categoryRepositoriesAsync = categoryRepositoriesAsync;
            _assetRepositoriesAsync = assetRepositoriesAsync;
        }

        public Task<PagedResponse<ReportResponseDto>> FilterReportAsync(PaginationFilter pagination, EnumLocation location, string? orderBy, bool? isDescending, string? route)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<ReportResponseDto>>> GetReportAsync(EnumLocation location)
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

            return new Response<IEnumerable<ReportResponseDto>> { Data = reportResponseDtos, Succeeded = true };
        }
    }
}