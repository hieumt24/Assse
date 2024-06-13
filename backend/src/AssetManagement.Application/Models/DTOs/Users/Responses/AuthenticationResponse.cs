namespace AssetManagement.Application.Models.DTOs.Users.Responses
{
    public class AuthenticationResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffCode { get; set; }
        public string Location { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}