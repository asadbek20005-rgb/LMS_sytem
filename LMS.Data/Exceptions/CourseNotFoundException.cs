namespace LMS.Data.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException():base("Course Not Found")
        {
            
        }
    }
}
