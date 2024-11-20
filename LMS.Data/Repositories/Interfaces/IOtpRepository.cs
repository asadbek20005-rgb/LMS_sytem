using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface IOtpRepository
    {
        Task Create(OTP oTP);
        Task CheckCodeExpired(int code);
    }
}
