﻿using AssetManagement.Application.Common;
using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Services;
using AssetManagement.Domain.Common.Settings;
using AssetManagement.Domain.Entites;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using AssetManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            service.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(connectionString));
        }
    }
}