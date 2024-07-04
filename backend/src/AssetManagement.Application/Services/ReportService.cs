using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Reports.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;
using ClosedXML.Excel;

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

        public async Task<byte[]> ExportReportToExcelAsync(EnumLocation location)
        {
            var reportResponse = await GetReportAsync(location);
            var reportData = reportResponse.Data;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AssetManagementReport");

                // Adding Headers
                var headers = new[] {"Category Name", "Total", "Assigned", "Available", "Not Available", "Waiting for Recycling", "Recycled" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = headers[i];
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                    worksheet.Cell(1, i + 1).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.Green;
                    worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(1, i + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                }

                // Adding Data
                int row = 2;
                foreach (var report in reportData)
                {
                    worksheet.Cell(row, 1).Value = report.CategoryName;
                    worksheet.Cell(row, 2).Value = report.Total;
                    worksheet.Cell(row, 3).Value = report.Assigned;
                    worksheet.Cell(row, 4).Value = report.Available;
                    worksheet.Cell(row, 5).Value = report.NotAvailable;
                    worksheet.Cell(row, 6).Value = report.Waitingforrecycling;
                    worksheet.Cell(row, 7).Value = report.Recycled;

                    for (int col = 1; col <= 7; col++)
                    {
                        worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        worksheet.Cell(row, col).Style.Fill.BackgroundColor = (row % 2 == 0) ? XLColor.Gray : XLColor.White;
                    }

                    row++;
                }

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}