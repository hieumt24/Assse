using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Models.DTOs.ReturnRequests.Reponses
{
    public class ReturnRequestResponseDto
    {
        public Guid Id { get; set; }
        public string? AssetCode { get; set; }
        public string? AssetName { get; set; }
        public string? RequestedByUserName { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string? AcceptedByUserName { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public EnumAssignmentStatus? Status { get; set; }
        public EnumLocation Location { get; set; }
    }
}