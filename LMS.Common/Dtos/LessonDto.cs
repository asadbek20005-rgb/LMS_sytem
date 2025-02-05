﻿namespace LMS.Common.Dtos
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<ContentDto>? Contents { get; set; }
    }
}
