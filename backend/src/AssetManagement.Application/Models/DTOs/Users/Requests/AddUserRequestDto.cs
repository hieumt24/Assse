using AssetManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Application.Models.DTOs.Users.Requests
{
    public class AddUserRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Min length of First Name is 2 characters")]
        [MaxLength(50, ErrorMessage = "Max length of First Name is 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "Min length of Last Name is 2 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Last Name is 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; } = DateTime.Now.Date;

        [Required]
        public GenderEnum Gender { get; set; } = GenderEnum.Unknown;

        [Required]
        public RoleType Role { get; set; } = RoleType.Staff;
    }
}