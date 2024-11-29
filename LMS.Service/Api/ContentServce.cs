using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using LMS.Service.Helpers;
using LMS.Service.MinioStorage;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Service.Api
{
    public class ContentServce(IContentRepository contentRepository, MinioStorageService minioStorageService)
    {
        private readonly IContentRepository _contentRepository = contentRepository;
        public async Task<ContentDto> AddOrUpdateContent(Guid userId, Guid courseId, int lessonId, AddOrUpdateContentModel addOrUpdateContentModel)
        {
            try
            {

                var videoFile = addOrUpdateContentModel.FormFile;
                ContentHelper.IsVideo(videoFile);
                var data = await ContentHelper.GetBytes(videoFile);
                var folderPath = Path.Combine("wwwroot", "Videos");
                var filePath = Path.Combine(folderPath, videoFile.FileName);
                if (data is not null)
                    await System.IO.File.WriteAllBytesAsync(filePath, data);
                var newContent = new Content
                {
                    Name = videoFile.FileName,
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


        public bool DeleteFile(string fileName)
        {
            try
            {
                string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos");
                string filePath = Path.Combine(rootPath, fileName);

                if (System.IO.File.Exists(filePath))
                {

                    System.IO.File.Delete(filePath);
                    Console.WriteLine("Fayl muvaffaqiyatli o'chirildi: " + filePath);

                }

                Console.WriteLine("Fayl topilmadi: " + filePath);
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
                    var stream = new FileStream(filePath,FileMode.Open, FileAccess.Read);

                    return stream;

                }


                
                else
                {
                    throw new Exception("Fayl topilmadi.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}