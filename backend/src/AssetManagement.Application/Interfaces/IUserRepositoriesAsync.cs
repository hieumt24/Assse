using AssetManagement.Application.Common;
using AssetManagement.Domain.Entites;

namespace AssetManagement.Application.Interfaces
{
    public interface IUserRepositoriesAsync : IBaseRepositoryAsync<User>
    {
        string GeneratePassword(string userName, DateTime dateOfBirth);

        string GenerateUsername(string firstName, string lastName);
    }
}