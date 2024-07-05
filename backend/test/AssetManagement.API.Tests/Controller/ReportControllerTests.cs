using AssetManagement.API.Controllers;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Reports.Responses;
using AssetManagement.Application.Models.Filters;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AssetManagement.API.Tests.Controller
{
    public class ReportControllerTests
    {
        private readonly Mock<IReportServices> _mockReportServices;
        private readonly ReportController _reportController;

        public ReportControllerTests()
        {
            _mockReportServices = new Mock<IReportServices>();
            _reportController = new ReportController(_mockReportServices.Object);
        }

        [Fact]
        public async Task GetReport_ReturnsOkResult_WhenReportIsFound()
        {
            // Arrange
            var reportFilter = new ReportFIlter
            {
                Location = EnumLocation.HaNoi,
                Pagination = new PaginationFilter(),
                OrderBy = "",
                IsDescending = false
            };

            var reportData = new PagedResponse<List<ReportResponseDto>>
            {
                Data = new List<ReportResponseDto>
                {
                    new ReportResponseDto { CategoryId = Guid.NewGuid(), CategoryName = "Category1", Total = 10, Assigned = 5, Available = 3, NotAvailable = 1, WaitingForRecycling = 1, Recycled = 0 },
                    new ReportResponseDto { CategoryId = Guid.NewGuid(), CategoryName = "Category2", Total = 10, Assigned = 5, Available = 3, NotAvailable = 1, WaitingForRecycling = 1, Recycled = 0 }
                }
            };

            _mockReportServices.Setup(service => service.GetReportPaginationAsync(reportFilter.Location, reportFilter.Pagination, reportFilter.OrderBy, reportFilter.IsDescending, It.IsAny<string>()))
                               .ReturnsAsync(reportData);

            // Act
            var result = await _reportController.GetReport(reportFilter) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(reportData, result.Value);
        }

        [Fact]
        public async Task GetReport_ReturnsBadRequest_WhenReportIsNotFound()
        {
            // Arrange
            var reportFilter = new ReportFIlter
            {
                Location = EnumLocation.HaNoi,
                Pagination = new PaginationFilter { PageIndex = 1, PageSize = 10 },
                OrderBy = "CategoryName",
                IsDescending = false
            };

            _mockReportServices.Setup(service => service.GetReportPaginationAsync(reportFilter.Location, reportFilter.Pagination, reportFilter.OrderBy, reportFilter.IsDescending, It.IsAny<string>()))
                               .ReturnsAsync((PagedResponse<List<ReportResponseDto>>)null);

            // Act
            var result = await _reportController.GetReport(reportFilter) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ExportReportToExcel_ReturnsFileResult_WhenDataIsAvailable()
        {
            // Arrange
            var location = EnumLocation.HaNoi;
            var fileContents = new byte[] { 1, 2, 3, 4, 5 };

            _mockReportServices.Setup(service => service.ExportReportToExcelAsync(location))
                               .ReturnsAsync(fileContents);

            // Act
            var result = await _reportController.ExportReportToExcel(location) as FileContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.ContentType);
            Assert.Equal("Report.xlsx", result.FileDownloadName);
            Assert.Equal(fileContents, result.FileContents);
        }
    }
}
