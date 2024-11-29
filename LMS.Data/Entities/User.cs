using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required] public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        [Required]
        [RegularExpression(@"^(\+?998)?[-\s]?\(?\d{2}\)?[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$",
        ErrorMessage = "Telefon raqami Oʻzbekiston raqamlariga mos emas.")]
        public string PhoneNumber { get; set; } // Phone number must be unique
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int Code { get; set; }
        public required string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsBlocked { get; set; }

        public virtual List<User_Course>? Courses { get; set; }
        public virtual List<User_Course_Payment>? Payments { get; set; }
    }
}
