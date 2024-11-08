namespace LMS.Common.Models
{
    public class AddOrUpdateContentModel
    {
        public required string Name { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
    }
}
