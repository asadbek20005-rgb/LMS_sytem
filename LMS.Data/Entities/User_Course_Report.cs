using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities
{
    public class User_Course_Report
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }
}