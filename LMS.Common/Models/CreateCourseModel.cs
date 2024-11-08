namespace LMS.Common.Models
{
    public class CreateCourseModel
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required string Category { get; set; }

    }
}
