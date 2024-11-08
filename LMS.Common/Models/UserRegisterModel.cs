namespace LMS.Common.Models
{
    public class UserRegisterModel
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; } 
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
