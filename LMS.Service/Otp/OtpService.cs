using LMS.Common.OTPModels;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.JwtToken;
using LMS.Service.MemoryCache;

namespace LMS.Service.Otp
{
    public class OtpService(MemoryCacheService memoryCacheService, IOtpRepository otpRepository)
    {
        private readonly MemoryCacheService _memoryCacheService = memoryCacheService;
        private readonly IOtpRepository _otpRepository = otpRepository;

        public int GenerateCode(string phoneNumber)
        {
            var random = new Random();
            int code = random.Next(1111, 9999);
            _memoryCacheService.SetPhoneNumberCode(phoneNumber, code);
            return code;
        }

        public int GenerateCodeForAdmin(string username)
        {
            var random = new Random();
            int code = random.Next(1111, 9999);
            _memoryCacheService.SetUsernameCode(username, code);
            return code;
        }

        public object VerifyRegister(OtpModel otpModel)
        {
            var otpValue = _memoryCacheService.GetPhoneNumberCode(otpModel.PhoneNumber);
            return otpValue == null ? throw new Exception() : otpValue;
        }

        public object? VerifyLogin(OtpModel otpModel)
        {
            var otpValue = _memoryCacheService.GetPhoneNumberCode(otpModel.PhoneNumber);

            return otpModel;
        }


        public async Task CheckCodeExpired(int code)
        {
            await _otpRepository.CheckCodeExpired(code);
        }
    }
}
