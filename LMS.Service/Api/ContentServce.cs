using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using LMS.Service.Helpers;

namespace LMS.Service.Api
{
    public class ContentServce(IContentRepository contentRepository, ILessonRepository lessonRepository)
    {
        private readonly IContentRepository _contentRepository = contentRepository;
        private readonly ILessonRepository _lessonRepository = lessonRepository;
        public async Task<ContentDto> AddOrUpdateContent(Guid userId, Guid courseId, int lessonId, AddOrUpdateContentModel formFile)
        {
            try
            {
                await CheckingForIds(userId, courseId, lessonId);
                var videoFile = formFile.FormFile;
                ContentHelper.IsVideo(videoFile);
                var data = await ContentHelper.GetBytes(videoFile);
                var folderPath = Path.Combine("wwwroot", "Videos");
                var filePath = Path.Combine(folderPath, formFile.FileName);
                if (data is not null)
                    await System.IO.File.WriteAllBytesAsync(filePath, data);
                var newContent = new Content
                {
                    Name = formFile.FileName,
                    LessonId = lessonId,
                };
                await _contentRepository.AddOrUpdateContent(newContent);
                return newContent.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteFile(Guid userId, Guid courseId, int lessonId, int contentId)
        {
            try
            {
                var content = await _contentRepository.GetContentById(userId, courseId, lessonId, contentId);

                string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos");
                string filePath = Path.Combine(rootPath, content.Name);

                if (System.IO.File.Exists(filePath))
                {

                    System.IO.File.Delete(filePath);
                    return true;

                }

                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<ContentDto>> GetAllContents(Guid userId, Guid courseId, int lessonId)
        {
            try
            {
                var contents = await _contentRepository.GetAllContents(userId, courseId, lessonId);
                return contents.ParseToDtos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Admin

        public async Task<List<ContentDto>> GetContents()
        {
            var contents = await _contentRepository.GetContents();
            return contents.ParseToDtos();
        }

        public async Task<Stream> GetContent(Guid userId, Guid courseId, int lessonId, int contentId)
        {
            try
            {
                var content = await _contentRepository.GetContentById(userId, courseId, lessonId, contentId);
                var fileName = content?.Name;

                if (string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("Fayl nomi mavjud emas.");
                }

                var folderPath = Path.Combine("wwwroot", "Videos");
                var filePath = Path.Combine(folderPath, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    var ms = new MemoryStream();
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        await fileStream.CopyToAsync(ms);
                    }

                    ms.Position = 0;
                    return ms;
                }
                else
                {
                    return Stream.Null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private async Task CheckingForIds(Guid userId, Guid courseId, int lessonId)
        {
            var checking = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
            if (checking == null)
            {
                throw new Exception("There is no given stuff");
            }
        }


    }
}