using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions.OTP;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class OtpReposiotry(AppDbContext appDbContext) : IOtpRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task CheckCodeExpired(int code)
        {
            var otp = await _context.OTP.SingleOrDefaultAsync(x => x.Code == code);
            if (otp != null)
                if (otp.IsExpired == true)
                    throw new ExpiredCodeException($"The code with {code} is expired");
        }



        public async Task Create(OTP oTP)
        {
            await _context.AddAsync(oTP);
            await _context.SaveChangesAsync();
        }
    }
}
