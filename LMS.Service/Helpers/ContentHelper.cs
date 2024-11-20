using LMS.Common.Constants;
using LMS.Data.Exceptions.Content;
using Microsoft.AspNetCore.Http;

namespace LMS.Service.Helpers
{
    public static class ContentHelper
    {
        public static void IsVideo(IFormFile formFile)
        {
            var check = formFile.ContentType == Constants.GP3 ||
                formFile.ContentType == Constants.MP4 ||
                formFile.ContentType == Constants.QuickTime ||
                formFile.ContentType == Constants.WebM ||
                formFile.ContentType == Constants.WMV ||
                formFile.ContentType == Constants.OGG ||
                formFile.ContentType == Constants.AVI ||
                formFile.ContentType == Constants.MPEG ||
                formFile.ContentType == Constants.GP3;

            if (!check)
                throw new VedioNotFoundException();
        }


        public static async Task<byte[]?> GetBytes(IFormFile formFile)
        {
            var ms = new MemoryStream();
            await formFile.CopyToAsync(ms);
            var data = ms.ToArray();
            var checking = data.Length == 0 || data is  null;
            if (checking)
                return null;
            else
                return data;

        }
    }
}
