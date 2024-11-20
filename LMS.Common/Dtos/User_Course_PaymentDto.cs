namespace LMS.Common.Dtos
{
    public class User_Course_PaymentDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public required decimal Amount { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}