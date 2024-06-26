using AssetManagement.Application.Filter;
using AssetManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Application.Models.Filters
{
    public class ReturnRequestFilter
    {
        public PaginationFilter pagination { get; set; }
        public EnumReturnRequestStatus? returnStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime? returnDate { get; set; }

        public string? search { get; set; }
        public string? orderBy { get; set; }
        public bool? isDescending { get; set; } = false;
        public EnumLocation location { get; set; }
    }
}