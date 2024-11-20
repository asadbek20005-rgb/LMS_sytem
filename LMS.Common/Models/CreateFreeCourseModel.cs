namespace LMS.Common.Models
{
    public class CreateFreeCourseModel
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Category { get; set; }
        public decimal Price = 0;
    }
}
