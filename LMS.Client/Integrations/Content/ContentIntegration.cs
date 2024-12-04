using LMS.Client.Helper;
using LMS.Common.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Content
{
    public class ContentIntegration(HttpClient httpClient, TokenHelper tokenHelper) : IContentIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly TokenHelper _tokenHelper = tokenHelper;
        public async Task<(HttpStatusCode, string)> GetClientContent(Guid courseId, int lessonId, int contentId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/clients/clientId/courses/{courseId}/lessons/{lessonId}ClientContents/{contentId}";
            var response = await _httpClient.GetAsync(url);
            var stream = response.RequestMessage?.RequestUri?.ToString();

            return new(response.StatusCode, stream);
        }

        private async Task<string> ConvertStreamToBlobUrl(Stream stream, string contentType)
        {
            if (stream.CanRead)
            {
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        // Streamni MemoryStreamga to'g'ri o'qish
                        await stream.CopyToAsync(ms);

                        // MemoryStreamni boshidan o'qish uchun kursorni qayta joylashtirish
                        ms.Seek(0, SeekOrigin.Begin);

                        // Data arrayni olish va Base64 formatiga o'tkazish
                        var data = ms.ToArray();
                        var base64 = Convert.ToBase64String(data);

                        return $"data:{contentType};base64,{base64}";
                    }
                }
                catch (Exception ex)
                {
                    // Exceptionni ushlash va diagnostika
                    Console.WriteLine($"Error converting stream: {ex.Message}");
                    return string.Empty;
                }
            }

            return string.Empty;
        }


        public async Task<Tuple<HttpStatusCode, List<ContentDto>>> GetClientContents(Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/clients/clientId/courses/{courseId}/lessons/{lessonId}ClientContents";
            var response = await _httpClient.GetAsync(url);
            var contentDtos = await response.Content.ReadFromJsonAsync<List<ContentDto>>();
            if (contentDtos is null)
                return new(HttpStatusCode.NotFound, new List<ContentDto>());
            return new(response.StatusCode, contentDtos);
        }

        public async Task<Tuple<HttpStatusCode, List<ContentDto>>> GetOwnerContents(Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/owners/ownerId/courses/{courseId}/lessons/{lessonId}/OwnerContents";
            var response = await _httpClient.GetAsync(url);
            var contentDtos = await response.Content.ReadFromJsonAsync<List<ContentDto>>();
            if (contentDtos is null)
                return new(HttpStatusCode.NotFound, new List<ContentDto>());
            return new(response.StatusCode, contentDtos);
        }

        public async Task<(HttpStatusCode, string)> GetOwnerContent(Guid courseId, int lessonId, int contentId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/owners/ownerId/courses/{courseId}/lessons/{lessonId}/OwnerContents/{contentId}";
            var response = await _httpClient.GetAsync(url);
            var stream = response.RequestMessage?.RequestUri?.ToString();

            return new(response.StatusCode, stream);
        }

        public async Task<HttpStatusCode> CreateOwnerContent(Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/owners/ownerId/courses/{courseId}/lessons/{lessonId}/OwnerContents";
            var response = await _httpClient.PostAsJsonAsync();
        }
    }
}
