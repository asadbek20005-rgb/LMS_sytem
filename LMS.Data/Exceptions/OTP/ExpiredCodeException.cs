namespace LMS.Data.Exceptions.OTP
{
    public class ExpiredCodeException : Exception
    {
        public ExpiredCodeException(string message):base(message)
        {
            
        }
    }
}
