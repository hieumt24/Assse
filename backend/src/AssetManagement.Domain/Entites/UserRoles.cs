using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Domain.Entites
{
    public class UserRoles
    {
        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}