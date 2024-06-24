using AssetManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Application.Models.DTOs.Assignment.Request
{
    public class AddAssignmentRequestDto
    {
        [Required]
        public Guid AssignedIdTo { get; set; }
        [Required]
        public Guid AssignedIdBy { get; set; }
        [Required]
        public Guid AssetId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime AssignedDate { get; set; }
        public string Note { get; set; } = string.Empty;

        public EnumLocation Location { get; set; }
        public EnumAssignmentStatus Status { get; set; } = EnumAssignmentStatus.WaitingForAcceptance;

    }
}
