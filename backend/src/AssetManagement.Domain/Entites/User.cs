using AssetManagement.Domain.Common.Models;
using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Domain.Entites
{
    public class User : BaseEntity
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
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; } = DateTime.Now.Date;

        [Required]
        public GenderEnum Gender { get; set; } = GenderEnum.Unknown;

        [Required]
        public RoleType Role { get; set; } = RoleType.Staff;

        [Required]
        [RegularExpression(@"^SD\d{4}$", ErrorMessage = "StaffCode must be in the format SDxxxx where xxxx are digits.")]
        public string StaffCode { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        //[RegularExpression(@"^[a-z]{2,}@[0-3][0-9][0-1][0-9]\d{4}$", ErrorMessage = "Password must be in the format [username]@[DOB in ddmmyyyy]")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public EnumLocation Location { get; set; }

        [Required]
        public bool IsFirstTimeLogin { get; set; } = true;

        public void GenerateStaffCode(int number)
        {
            StaffCode = $"SD{number:D4}";
        }

        public static string GenerateUsername(string firstName, string lastName)
        {
            // Normalize names to lower case
            firstName = firstName.ToLower();
            lastName = lastName.ToLower();

            // Get the first letter of each part of the last name
            var lastNameParts = lastName.Split(' ');
            var lastNameInitials = string.Join("", lastNameParts.Select(part => part[0]));

            // Combine first name and initials of last names
            string username = firstName + lastNameInitials;

            return username;
        }

        public void GeneratePassword()
        {
            string dob = DateOfBirth.ToString("ddMMyyyy");
            Password = $"{Username}@{dob}";
        }
    }
}
