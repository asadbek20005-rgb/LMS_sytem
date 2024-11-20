namespace LMS.Data.Exceptions.User
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User Not Found")
        {

        }
    }
}
