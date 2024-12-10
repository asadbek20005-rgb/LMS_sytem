using LMS.Client.LocalStorage;
using LMS.Common.Models.AccountModels;
using LMS.Common.OTPModels;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Client
{
    public class ClientIntegration(HttpClient httpClient, LocalStorageService localStorageService) : IClientIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly LocalStorageService _localStorageService = localStorageService;

        public async Task<Tuple<HttpStatusCode, string>> Login(ClientLoginModel clientLoginModel)
        {
            string url = "/api/Clients/account/login";
            var response = await _httpClient.PostAsJsonAsync(url, clientLoginModel);
            var code = await response.Content.ReadAsStringAsync();
            return new(response.StatusCode, code);
        }

        public async Task<Tuple<HttpStatusCode, string>> Register(ClientRegisterModel clientRegisterModel)
        {
            string url = "/api/Clients/account/register";
            var response = await _httpClient.PostAsJsonAsync(url, clientRegisterModel);
            string code = await response.Content.ReadAsStringAsync();
            return new(response.StatusCode,code);
        }

        public async Task<HttpStatusCode> VerifyLogin(OtpModel otpModel)
        {
            string url = "/api/Clients/account/verify-login";
            var response = await _httpClient.PostAsJsonAsync(url, otpModel);
            var token = await response.Content.ReadAsStringAsync();
            await _localStorageService.SetToken(token);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> VerifyRegister(OtpModel otpModel)
        {
            string url = "/api/Clients/account/verify-register";
            var response = await _httpClient.PostAsJsonAsync(url,otpModel);
            return response.StatusCode;
        }
    }
}