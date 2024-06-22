using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class UserRepository : BaseRepositoryAsync<User>, IUserRepositoriesAsync
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            var user = await _dbContext.Users
           .Where(u => u.Username.ToLower() == username.ToLower())
           .FirstOrDefaultAsync();

            if (user != null && user.Username.Equals(username, StringComparison.Ordinal))
            {
                return user;
            }

            return null;
        }

        public string GeneratePassword(string userName, DateTime dateOfBirth)
        {
            string dob = dateOfBirth.ToString("ddMMyyyy");
            return $"{userName}@{dob}";
        }

        public async Task<string> GenerateUsername(string firstName, string lastName)
        {
            // Normalize names to lower case
            firstName = firstName.ToLower().Replace(" ", "");
            lastName = string.Join(" ", lastName.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));

            // Get the first letter of each part of the last name
            var lastNameParts = lastName.Split(' ');
            var lastNameInitials = string.Join("", lastNameParts.Select(part => part[0]));

            // Combine first name and initials of last names
            string baseUserName = firstName + lastNameInitials;

            string username = baseUserName;
            int count = 1;

            while (await IsUsernameExist(username))
            {
                username = baseUserName + count;
                count++;
            }

            return username;
        }

        public async Task<RoleType> GetRoleAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user.Role;
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }

        public IQueryable<User> Query(EnumLocation adminLocation)
        {
            return _dbContext.Users.Where(x => x.Location == adminLocation);
        }

        public async Task<IQueryable<User>> FilterUserAsync(EnumLocation adminLocation, string? search, RoleType? roleType)
        {
            //check adminlocatin
            var query = _dbContext.Users.Where(x => x.Location == adminLocation);

            //search
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.ToLower())
                                                        || x.LastName.ToLower().Contains(search.ToLower())
                                                        || x.Username.ToLower().Contains(search.ToLower()));
            }

            //filter by role

            if (roleType != null)
            {
                query = query.Where(x => x.Role == roleType);
            }

            return query;
        }

        public async Task<User> UpdateUserAysnc(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            _dbContext.Entry(existingUser).Property(e => e.StaffCodeId).IsModified = false;

            await _dbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}