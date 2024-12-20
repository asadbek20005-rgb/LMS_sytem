﻿using LMS.Client.Helper;
using LMS.Common.Dtos;
using LMS.Common.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Content
{
    public class ContentIntegration(HttpClient httpClient, TokenHelper tokenHelper) : IContentIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly TokenHelper _tokenHelper = tokenHelper;
        public async Task<(Stream, string, string)> GetClientContent(Guid courseId, int lessonId, int contentId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/clients/clientId/courses/{courseId}/lessons/{lessonId}/ClientContents/{contentId}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var contentDisposition = response.Content.Headers.ContentDisposition;
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
                var fileName = contentDisposition?.FileName ?? "unknown";

                return (contentStream, fileName, contentType);
            }
            throw new NotImplementedException();
        }



        public async Task<Tuple<HttpStatusCode, List<ContentDto>>> GetClientContents(Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/clients/clientId/courses/{courseId}/lessons/{lessonId}/ClientContents";
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

        public async Task<(Stream, string, string)> GetOwnerContent(Guid courseId, int lessonId, int contentId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/owners/ownerId/courses/{courseId}/lessons/{lessonId}/OwnerContents/{contentId}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var contentDisposition = response.Content.Headers.ContentDisposition;
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
                var fileName = contentDisposition?.FileName ?? "unknown";

                return (contentStream, fileName, contentType);
            }
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> CreateOwnerContent(Guid courseId, int lessonId, AddOrUpdateContentModel addOrUpdateContent, IBrowserFile file)
        {
            await _tokenHelper.AddTokenToHeader();
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(addOrUpdateContent.FileName), "FileName");
            var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 300 * 1024 * 1024));
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "FormFile", file.Name);
            string url = $"/api/owners/ownerId/courses/{courseId}/lessons/{lessonId}/OwnerContents";
            var response = await _httpClient.PostAsync(url, content);
            return response.StatusCode;
        }
    }
}
