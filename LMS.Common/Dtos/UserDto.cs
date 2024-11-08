namespace LMS.Common.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; } 
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<User_CourseDto>? Courses { get; set; }
        public List<User_Course_PaymentDto>? Payments { get; set; }
    }
}
