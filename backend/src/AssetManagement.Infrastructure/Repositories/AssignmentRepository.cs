using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;

namespace AssetManagement.Infrastructure.Repositories
{
    public class AssignmentRepository : BaseRepositoryAsync<Assignment>, IAssignmentRepositoriesAsync
    {
        public AssignmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
