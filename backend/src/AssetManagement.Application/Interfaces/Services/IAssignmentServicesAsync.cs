using AssetManagement.Application.Models.DTOs.Assignment;
using AssetManagement.Application.Models.DTOs.Assignment.Request;
using AssetManagement.Application.Wrappers;

namespace AssetManagement.Application.Interfaces.Services
{
    public interface IAssignmentServicesAsync
    {
        Task<Response<AssignmentDto>> AddAssignmentAsync(AddAssignmentRequestDto request);
        Task<Response<AssignmentDto>> GetAssignmentByIdAsync(Guid assignmentId);
        Task<Response<AssignmentDto>> EditAssignmentAsync(EditAssignmentRequestDto request);
        Task<Response<AssignmentDto>> DeleteAssignmentAsync(Guid assignmentId);
        Task<Response<List<AssignmentDto>>> GetAllAssignmentsAsync();
    }
}
