using System.ComponentModel.DataAnnotations;

namespace LMS.Common.Models.AccountModels
{
    public class ClientRegisterModel
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
