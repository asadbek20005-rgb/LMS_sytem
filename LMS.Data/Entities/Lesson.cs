namespace LMS.Data.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual List<Content>? Contents { get; set; }
    }
}
