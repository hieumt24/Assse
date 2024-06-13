using AssetManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Application.Models.DTOs.Users.Requests
{
    public class AddUserRequestDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }

        public GenderEnum Gender { get; set; }

<<<<<<< HEAD
        public RoleType Role { get; set; } 
=======
        public RoleType Role { get; set; } = RoleType.Staff;
>>>>>>> 7ee37336f7f0ecff18698061f2b2699022809a98

        public EnumLocation Location { get; set; }
    }
}