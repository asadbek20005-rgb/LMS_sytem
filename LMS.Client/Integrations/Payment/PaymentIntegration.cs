using LMS.Client.Helper;
using LMS.Common.Models;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Payment
{
    public class PaymentIntegration(TokenHelper tokenHelper, HttpClient httpClient) : IPaymentIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly TokenHelper _tokenHelper = tokenHelper;
        public async Task<HttpStatusCode> Pay(Guid courseId, CreateUser_Course_Payment createUser_Course_Payment, CreateCardInfoModel createCardInfoModel)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/clients/clientId/courses/{courseId}/Payments";
            var response = await _httpClient.PostAsJsonAsync(url, createUser_Course_Payment);
            if (response.IsSuccessStatusCode)
            {
                string urlCard = "/api/clients/clientId/Cards";
                var response2 = await _httpClient.PostAsJsonAsync(urlCard, createCardInfoModel);
                return response2.StatusCode;
            }
            return response.StatusCode;

        }
    }
}
