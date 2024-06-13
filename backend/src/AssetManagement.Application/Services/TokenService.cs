using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Common.Settings;
using AssetManagement.Domain.Entites;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManagement.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;

        public TokenService(JWTSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateJwtToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.UserData, user.StaffCode),
                new Claim(ClaimTypes.Locality, user.Location.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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