namespace LMS.Data.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson? Lesson { get; set; }
    }
}
