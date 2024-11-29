using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities
{
    public class User_Course_Payment
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User? User {  get; set; }
        
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }

        public required decimal Amount { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
