using LMS.Client.LocalStorage;

namespace LMS.Client.Helper
{
    public class TokenHelper(LocalStorageService localStorageService, HttpClient httpClient)
    {
        private readonly LocalStorageService _localStorageService = localStorageService;
        private readonly HttpClient _httpClient = httpClient;

        public  async Task AddTokenToHeader()
        {
            var token = await _localStorageService.GetToken();
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
