using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Domain.Common.Settings;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManagement.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;

        public TokenService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateJwtToken(User user, RoleType role)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("StaffCode", user.StaffCode),
                new Claim("Location", user.Location.ToString()),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim("IsFirstTimeLogin", user.IsFirstTimeLogin.ToString().ToLowerInvariant()),
                new Claim("DateOfBirth", user.DateOfBirth.ToString("dd-MM-yyyy"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                               _jwtSettings.Issuer,
                               _jwtSettings.Audience,
                               claims,
                               expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
                               signingCredentials: credentials
                          );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}