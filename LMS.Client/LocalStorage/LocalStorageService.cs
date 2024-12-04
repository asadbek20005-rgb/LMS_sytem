using Blazored.LocalStorage;
using LMS.Common.Constants;

namespace LMS.Client.LocalStorage
{
    public class LocalStorageService(ILocalStorageService localStorageService)
    {
        private readonly ILocalStorageService _storageService = localStorageService;


        public async Task SetToken(string token)
        {
            await _storageService.SetItemAsync(Constants.TokenKey, token);
        }

        public async Task<string> GetToken()
        {
            string? token = await _storageService.GetItemAsync<string>(Constants.TokenKey);
            if (!string.IsNullOrEmpty(token))
                return token;
            return string.Empty;
        }



        public async Task RemoveToken()
        {
           await _storageService.RemoveItemAsync(Constants.TokenKey);
        }
    }
}
