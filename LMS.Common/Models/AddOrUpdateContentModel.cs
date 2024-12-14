using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS.Common.Models
{
    public class AddOrUpdateContentModel
    {
        [Required]
        public  IFormFile FormFile {  get; set; }
        public string FileName { get; set; }
    }
}
