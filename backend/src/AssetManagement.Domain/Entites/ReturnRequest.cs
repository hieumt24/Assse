using AssetManagement.Domain.Common.Models;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Domain.Entites
{
    public class ReturnRequest : BaseEntity
    {
        public Guid AssignmentId { get; set; }
        public Assignment? Assignment { get; set; }
        public Guid RequestedBy { get; set; }
        public User? RequestedUser { get; set; }
        public Guid AcceptedBy { get; set; }
        public User? AcceptedUser { get; set; }
        public DateTime ReturnedDate { get; set; }
        public EnumReturnRequestStatus ReturnStatus { get; set; } = EnumReturnRequestStatus.WaitingForReturning;
    }
}