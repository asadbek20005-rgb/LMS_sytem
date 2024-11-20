namespace LMS.Data.Exceptions.User
{
    public class SameUserExistException : Exception
    {
        public SameUserExistException(string message) : base(message)
        {

        }
    }
}
