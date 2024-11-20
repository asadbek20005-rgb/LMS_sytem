namespace LMS.Service.File;

using Microsoft.AspNetCore.Hosting;
using System.IO;

public class FileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public bool DeleteVideo(string fileName)
    {
        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "videos", fileName);

        if (File.Exists(filePath)) 
        {
            File.Delete(filePath); 
            return true;
        }

        return false; 
    }
}
