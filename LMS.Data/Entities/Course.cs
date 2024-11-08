namespace LMS.Data.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required string Category { get; set; }
        public DateTime CreatedDate { get; set; }


        public List<User_Course>? Users { get; set; }
        public List<User_Course_Payment>? Payments { get; set; }
        public List<Lesson>? Lessons { get; set; }
    }
}
