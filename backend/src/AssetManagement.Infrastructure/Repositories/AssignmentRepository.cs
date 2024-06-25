﻿using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class AssignmentRepository : BaseRepositoryAsync<Assignment>, IAssignmentRepositoriesAsync
    {
        public AssignmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Assignment>> FilterAssignment(EnumLocation adminLocation, string? search, EnumAssignmentStatus? assignmentStatus, DateTime? assignedDate)
        {
            var query = _dbContext.Assignments.Include(x => x.Asset).Where(x => x.Location == adminLocation);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Asset.AssetCode.ToLower().Contains(search)
                                        || x.Asset.AssetName.ToLower().Contains(search)
                                        || x.AssignedBy.Username.ToLower().Contains(search)
                                        || x.AssignedTo.Username.ToLower().Contains(search));
            }
            if (assignmentStatus.HasValue)
            {
                query = query.Where(x => x.Status == assignmentStatus);
            }
            if (assignedDate.HasValue)
            {
                query = query.Where(x => x.AssignedDate.Date == assignedDate.Value.Date);
            }
            return query;
        }
    }
}