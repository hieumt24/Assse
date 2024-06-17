using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Mappings;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Services;
using AssetManagement.Application.Validations;
using AssetManagement.Domain.Common.Settings;
using AssetManagement.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AssetManagement.Application
{
    public class ServiceExtensions
    {
        public static void ConfigureServices(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IAssetServiceAsync, AssetServiceAsync>();

            service.AddAutoMapper(typeof(GeneralProfile));
            service.AddScoped<IValidator<AddUserRequestDto>, AddUserRequestValidation>();
            service.AddScoped<IValidator<EditUserRequestDto>, EditUserRequestValidation>();
            service.AddScoped<IValidator<ChangePasswordRequest>, PasswordValidation>();
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IAccountServicecs, AccountService>();
            service.AddScoped<IUserServiceAsync, UserServiceAsync>();
            service.AddSingleton<IUriService>(options =>
            {
                var accessor = options.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            var jwtSettings = service.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            service.AddSingleton(jwtSettings);
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                });

            service.AddAuthorization(options =>
            {
                options.AddPolicy($"{RoleType.Admin}", policy => policy.RequireRole("Admin"));
                options.AddPolicy($"{RoleType.Staff}", policy => policy.RequireRole("Staff"));
            });
        }
    }
}