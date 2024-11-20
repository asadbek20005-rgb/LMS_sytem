namespace LMS.Data.Exceptions.User
{
    public class UserBlockedException :Exception
    {
        public UserBlockedException():base("User was blocked")
        {
            
        }
    }
}
