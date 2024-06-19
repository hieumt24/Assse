using AssetManagement.Application.Common;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces.Repositories
{
    public interface IUserRepositoriesAsync : IBaseRepositoryAsync<User>
    {
        Task<User> FindByUsernameAsync(string username);

        string GeneratePassword(string userName, DateTime dateOfBirth);

        Task<string> GenerateUsername(string firstName, string lastName);

        Task<RoleType> GetRoleAsync(Guid userId);

        Task<bool> IsUsernameExist(string username);

        Task<User> UpdateUserAysnc(User user);


    }
}