using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Models.DTOs.ReturnRequests.Request
{
    public class ChangeStateReturnRequestDto
    {
        public Guid AssignmentId { get; set; }
        public EnumAssignmentState NewState { get; set; }
    }
}
