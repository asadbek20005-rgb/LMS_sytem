namespace LMS.Data.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User Not Found")
        {

        }
    }
}
