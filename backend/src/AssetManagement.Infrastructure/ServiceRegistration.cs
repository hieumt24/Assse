using AssetManagement.Application.Common;
using AssetManagement.Application.Interfaces;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagement.Infrastructure
{
    public class ServiceRegistration
    {
        public static void Configure(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            service.AddScoped<IUserRepositoriesAsync, UserRepository>();
        }
    }
}