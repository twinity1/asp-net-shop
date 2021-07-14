using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DyShop.Services
{
    public class FileHandlerService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string FileDirectoryPath = "files";

        public string FileDirectory => FileDirectoryPath;

        public FileHandlerService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        public async Task<string> SaveFile(IFormFile file, string topic)
        {
            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
            
            var webRootPath = _webHostEnvironment.WebRootPath;
            
            var relativePathDirectory = Path.Combine(FileDirectoryPath, topic);
            var relativePath = Path.Combine(relativePathDirectory, fileName);

            Directory.CreateDirectory(Path.Combine(webRootPath, relativePathDirectory));

            var absolutePath = Path.Combine(webRootPath, relativePath);
            
            await using Stream fileStream = new FileStream(absolutePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return relativePath;
        }

        public void RemoveFile(string relativePath)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            
            File.Delete(Path.Combine(webRootPath, relativePath));
        }
    }
}