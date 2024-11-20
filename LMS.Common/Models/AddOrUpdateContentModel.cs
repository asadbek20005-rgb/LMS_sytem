using Microsoft.AspNetCore.Http;

namespace LMS.Common.Models
{
    public class AddOrUpdateContentModel
    {
        public required IFormFile FormFile {  get; set; }
    }
}
