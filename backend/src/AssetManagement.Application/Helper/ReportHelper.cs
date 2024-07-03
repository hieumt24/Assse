using AssetManagement.Application.Filter;
using AssetManagement.Application.Models.DTOs.Reports.Responses;
using AssetManagement.Domain.Common.Specifications;
using System.Linq.Expressions;

namespace AssetManagement.Application.Helper
{
    public static class ReportHelper
    {
        public static List<ReportResponseDto> ApplySorting(List<ReportResponseDto> reports, string? orderBy, bool? isDescending)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return reports.OrderBy(x => x.CategoryName).ToList();
            }

            return orderBy.ToLower() switch
            {
                "categoryname" => isDescending.HasValue && isDescending.Value ? reports.OrderByDescending(x => x.CategoryName).ToList() : reports.OrderBy(x => x.CategoryName).ToList(),
                "total" => isDescending.HasValue && isDescending.Value ? reports.OrderByDescending(x => x.Total).ToList() : reports.OrderBy(x => x.Total).ToList(),
                "assigned" => isDescending.HasValue && isDescending.Value ? reports.OrderByDescending(x => x.Assigned).ToList() : reports.OrderBy(x => x.Assigned).ToList(),
                "available" => isDescending.HasValue && isDescending.Value ? reports.OrderByDescending(x => x.Available).ToList() : reports.OrderBy(x => x.Available).ToList(),
                "notavailable" => isDescending.HasValue && isDescending.Value ? reports.OrderByDescending(x => x.NotAvailable).ToList() : reports.OrderBy(x => x.NotAvailable).ToList(),
                "waitingforrecycling" => isDescending.HasValue && isDescending.Value ? reports.OrderByDescending(x => x.Waitingforrecycling).ToList() : reports.OrderBy(x => x.Waitingforrecycling).ToList(),
                "recycled" => isDescending.HasValue && isDescending.Value ? reports.OrderByDescending(x => x.Recycled).ToList() : reports.OrderBy(x => x.Recycled).ToList(),
                _ => reports.OrderBy(x => x.CategoryName).ToList(),
            };
        }
    }
}