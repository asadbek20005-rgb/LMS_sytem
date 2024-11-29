using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities
{
    public class User_Course
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }


        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }


        public bool IsOwner { get; set; }
        public bool IsPayed { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
