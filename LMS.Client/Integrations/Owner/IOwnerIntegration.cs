using LMS.Common.Models.AccountModels;
using LMS.Common.OTPModels;
using System.Net;

namespace LMS.Client.Integrations.Owner
{
    public interface IOwnerIntegration
    {
        Task<Tuple<HttpStatusCode, string>> Register(OwnerRegisterModel ownerRegisterModel);
        Task<HttpStatusCode> VerifyRegister(OtpModel otpModel);
        Task<Tuple<HttpStatusCode, string>> Login(OwnerLoginModel ownerLoginModel);
        Task<HttpStatusCode> VerifyLogin(OtpModel otpModel);
    }
}
