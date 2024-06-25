using AssetManagement.Application.Filter;
using AssetManagement.Application.Models.DTOs.Assignment;
using AssetManagement.Application.Models.DTOs.Assignment.Request;
using AssetManagement.Application.Models.DTOs.Assignment.Response;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces.Services
{
    public interface IAssignmentServicesAsync
    {
        Task<Response<AssignmentDto>> AddAssignmentAsync(AddAssignmentRequestDto request);

        Task<Response<AssignmentDto>> GetAssignmentByIdAsync(Guid assignmentId);

        Task<Response<AssignmentDto>> EditAssignmentAsync(EditAssignmentRequestDto request);

        Task<Response<AssignmentDto>> DeleteAssignmentAsync(Guid assignmentId);

        Task<PagedResponse<List<AssignmentResponseDto>>> GetAllAssignmentsAsync(PaginationFilter paginationFilter, string? search, EnumAssignmentStatus? assignmentStatus, DateTime? assignedDate, EnumLocation adminLocation, string? orderBy, bool? isDescending, string? route);
    }
}