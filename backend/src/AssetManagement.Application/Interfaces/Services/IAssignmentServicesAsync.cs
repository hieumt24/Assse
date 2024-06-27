using AssetManagement.Application.Filter;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.Assignments.Reques;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.Assignments.Requests;
using AssetManagement.Application.Models.DTOs.Assignments.Response;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces.Services
{
    public interface IAssignmentServicesAsync
    {
        Task<Response<AssignmentDto>> AddAssignmentAsync(AddAssignmentRequestDto request);

        Task<Response<AssignmentResponseDto>> GetAssignmentByIdAsync(Guid assignmentId);

        Task<Response<AssignmentDto>> EditAssignmentAsync(EditAssignmentRequestDto request);

        Task<Response<AssignmentDto>> DeleteAssignmentAsync(Guid assignmentId);

        Task<Response<List<AssignmentResponseDto>>> GetAssignmentsOfUser(Guid userId);

        Task<Response<AssignmentDto>> ChangeAssignmentStateAsync(ChangeStateAssigmentDto request);

        Task<PagedResponse<List<AssignmentResponseDto>>> GetAllAssignmentsAsync(PaginationFilter paginationFilter, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate, EnumLocation adminLocation, string? orderBy, bool? isDescending, string? route);
    }
}