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

        public RoleType RoleId { get; set; } 

        public EnumLocation Location { get; set; }
    }
}