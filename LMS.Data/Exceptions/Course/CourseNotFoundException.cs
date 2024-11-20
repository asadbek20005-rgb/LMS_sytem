namespace LMS.Data.Exceptions.Course
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException() : base("Course Not Found")
        {

        }
    }
}
