using LMS.Common.Models.AccountModels;
using LMS.Common.OTPModels;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Owner
{
    public class OwnerIntegration(HttpClient httpClient) : IOwnerIntegration
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<Tuple<HttpStatusCode, string>> Login(OwnerLoginModel ownerLoginModel)
        {
            string url = "/api/Owners/account/login";
            var response = await _httpClient.PostAsJsonAsync(url, ownerLoginModel);
            var code = await response.Content.ReadAsStringAsync();
            return new(response.StatusCode, code);
        }

        public async Task<Tuple<HttpStatusCode, string>> Register(OwnerRegisterModel ownerRegisterModel)
        {
            string url = "/api/Owners/account/register";
            var response = await _httpClient.PostAsJsonAsync(url, ownerRegisterModel);
            string code = await response.Content.ReadAsStringAsync();
            return new(response.StatusCode, code);
        }

        public async Task<HttpStatusCode> VerifyLogin(OtpModel otpModel)
        {
            string url = "/api/Owners/account/verify-login";    
            var response = await _httpClient.PostAsJsonAsync(url, otpModel);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> VerifyRegister(OtpModel otpModel)
        {
            string url = "/api/Owners/account/verify-register";
            var response = await _httpClient.PostAsJsonAsync(url, otpModel);
            return response.StatusCode;
        }
    }
}
