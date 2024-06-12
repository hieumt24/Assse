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
        public DateTime JoinedDate { get; set; } = DateTime.Now.Date;

        public GenderEnum Gender { get; set; } = GenderEnum.Unknown;

        public RoleType RoleId { get; set; } = RoleType.Staff;

        public EnumLocation Location { get; set; } = EnumLocation.HaNoi;
    }
}