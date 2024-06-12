using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Models.DTOs.Users.Responses
{
    public class UserResponseDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoinedDate { get; set; } 

        public GenderEnum Gender { get; set; } 

        public RoleType Role { get; set; } 

        public string StaffCode { get; set; } 

        public string Username { get; set; } 

        public EnumLocation Location { get; set; }


    }
}
