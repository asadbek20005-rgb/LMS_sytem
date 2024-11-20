using LMS.Common.Models.AccountModels;
using LMS.Common.OTPModels;
using System.Net;

namespace LMS.Client.Integrations.Client
{
    public interface IClientIntegration
    {
        Task<Tuple<HttpStatusCode, string>> Register(ClientRegisterModel clientRegisterModel);
        Task<HttpStatusCode> VerifyRegister(OtpModel otpModel);
        Task<Tuple<HttpStatusCode, string>> Login(ClientLoginModel clientLoginModel);
        Task<HttpStatusCode> VerifyLogin(OtpModel otpModel);
    }
}

//Task<Tuple<HttpStatusCode, object>> Register(RegisterUserModel loginUser);
