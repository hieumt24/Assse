using AssetManagement.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Domain.Entites
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length of Role Name is 50 characters")]
        public string Name { get; set; } = string.Empty;

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}