using AssetManagement.Application.Common;
using AssetManagement.Domain.Entites;

namespace AssetManagement.Application.Interfaces.Repositories
{
    public interface IAssignmentRepositoriesAsync : IBaseRepositoryAsync<Assignment>
    {
        Task<IQueryable<Assignment>> FilterAssignment(EnumLocation adminLocation, string? search, EnumAssignmentStatus? assignmentStatus, DateTime? assignedDate);
    }
}