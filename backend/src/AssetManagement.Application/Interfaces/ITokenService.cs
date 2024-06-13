using AssetManagement.Domain.Entites;
using System.IdentityModel.Tokens.Jwt;

namespace AssetManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user, List<string> roles);
    }
}