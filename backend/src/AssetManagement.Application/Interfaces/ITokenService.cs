using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;

namespace AssetManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user, RoleType role);
    }
}