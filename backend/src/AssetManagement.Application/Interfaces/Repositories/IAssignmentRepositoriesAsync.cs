using AssetManagement.Application.Common;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Interfaces.Repositories
{
    public interface IAssignmentRepositoriesAsync : IBaseRepositoryAsync<Assignment>
    {
        Task<IQueryable<Assignment>> FilterAssignmentAsync(EnumLocation adminLocation, string? search, EnumAssignmentStatus? assignmentStatus, DateTime? assignedDate);
    }
}