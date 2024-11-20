using System.ComponentModel.DataAnnotations;

namespace LMS.Common.Models.AccountModels
{
    public class OwnerRegisterModel
    {
        public  string FirstName { get; set; }
        public string? LastName { get; set; }
        public  string PhoneNumber { get; set; }

        public  string Username { get; set; }
        public  string Password { get; set; }
        [Compare("Password")]
        public  string ConfirmPassword { get; set; }
    }
}
