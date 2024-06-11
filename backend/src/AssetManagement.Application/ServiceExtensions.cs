using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Mappings;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Services;
using AssetManagement.Application.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagement.Application
{
    public class ServiceExtensions
    {
        public static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IUserServiceAsync, UserServiceAsync>();
            service.AddAutoMapper(typeof(GeneralProfile));
            service.AddScoped<IValidator<AddUserRequestDto>, AddUserRequestValidation>();
        }
    }
}