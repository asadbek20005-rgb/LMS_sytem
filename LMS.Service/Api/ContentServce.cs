using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using LMS.Service.Helpers;
using LMS.Service.MinioStorage;

namespace LMS.Service.Api
{
    public class ContentServce(IContentRepository contentRepository,MinioStorageService minioStorageService)
    {
        private readonly IContentRepository _contentRepository = contentRepository;
        public async Task<ContentDto> AddOrUpdateContent(Guid userId, Guid courseId, int lessonId, AddOrUpdateContentModel addOrUpdateContentModel)
        {
            try
            {
                //var vedioFile = addOrUpdateContentModel.FormFile;
                //var contentType = vedioFile.ContentType;
                //long size = vedioFile.Length;
                //var fileName = Guid.NewGuid().ToString();
                //MemoryStream memoryStream = new MemoryStream();
                //await vedioFile.CopyToAsync(memoryStream);
                //await _minioStorageService.UploadFileAsync(fileName, memoryStream, contentType);

                //var newContent = new Content
                //{
                //    Name = fileName              
                //};

                //await _contentRepository.AddOrUpdateContent(newContent);
                //return newContent.ParseToDto();

                var videoFile = addOrUpdateContentModel.FormFile;
                ContentHelper.IsVideo(videoFile);
                var data = await ContentHelper.GetBytes(videoFile);
                var folderPath = Path.Combine("wwwroot", "Videos");
                var filePath = Path.Combine(folderPath, videoFile.FileName);
                if (data is not null)
                    await System.IO.File.WriteAllBytesAsync(filePath, data);
                var newContent = new Content
                {
                    Name = videoFile.Name,
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

        //public static async Task<byte[]> GetBytes(IFormFile file)
        //{

        //    var ms = new MemoryStream();
        //    await file.CopyToAsync(ms);
        //    var data = ms.ToArray();
        //    var checkData = data is null || data.Length == 0;
        //    if (checkData)
        //        return null;
        //    return data;
        //}


        //public static void IsFile(IFormFile file)
        //{
        //    var check = file.ContentType != Constants.PngType ||
        //        file.ContentType != Constants.JpgType;

        //    if (!check)
        //        throw new PhotoNotFound();
        //}
        //public async Task<byte[]> AddOrUpdatePhoto(Guid userId, IFormFile file)
        //{

        //    var user = await _unitOfWork.UserRepository.GetUserById(userId);
        //    StaticHelper.IsFile(file);
        //    var data = await StaticHelper.GetBytes(file);
        //    user.PhotoData = data;
        //    await _unitOfWork.UserRepository.UpdateUser(user);
        //    await Set();
        //    return data;
        //}
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

        public async Task<ContentDto> GetContent(Guid userId, Guid courseId, int lessonId, int contentId, string filename)
        {
            try
            {
                var content = await _contentRepository.GetContentById(userId, courseId, lessonId, contentId);
                return content.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}