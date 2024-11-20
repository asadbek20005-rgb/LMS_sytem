namespace LMS.Common.Dtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required string Category { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<LessonDto>? Lessons { get; set; }
        public List<User_CourseDto>? Users { get; set; }
        public List<User_Course_PaymentDto>? Payments { get; set; }
    }
}
