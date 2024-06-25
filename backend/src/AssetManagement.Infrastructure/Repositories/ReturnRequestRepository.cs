using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;

namespace AssetManagement.Infrastructure.Repositories
{
    public class ReturnRequestRepository : BaseRepositoryAsync<ReturnRequest>, IReturnRequestRepositoriesAsync 
    {
        public ReturnRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
