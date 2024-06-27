using AssetManagement.Application.Common;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces.Repositories
{
    public interface IAssignmentRepositoriesAsync : IBaseRepositoryAsync<Assignment>
    {
        Task<IQueryable<Assignment>> FilterAssignmentAsync(EnumLocation adminLocation, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate);

        Task<IQueryable<Assignment>> GetAssignmentsByUserId(Guid userId);

        Task<Assignment> GetAssignemntByIdAsync(Guid assignmentId);
    }
}