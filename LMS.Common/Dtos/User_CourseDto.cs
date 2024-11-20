namespace LMS.Common.Dtos
{
    public class User_CourseDto
    {
        public Guid UserId { get; set; }
    

        public Guid CourseId { get; set; }


        public bool IsOwner { get; set; }
        public bool IsPayed { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
