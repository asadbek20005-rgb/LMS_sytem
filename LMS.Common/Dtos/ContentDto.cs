namespace LMS.Common.Dtos
{
    public class ContentDto
    {
        public int Id { get; set; }
        public string? GoogleDriveFileId { get; set; }
        public required string Name { get; set; }
        public string? Url { get; set; }
        public LessonDto? Lesson { get; set; }
    }
}
