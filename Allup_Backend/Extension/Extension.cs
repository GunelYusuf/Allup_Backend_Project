using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Allup_Backend.Extension
{
    public static class Extension
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool IsCorrectSize(this IFormFile file, int size)
        {
            return file.Length / 1024 > size;
        }

        public async static Task<string> SaveImageAsync(this IFormFile file, string root, string folder)
        {
            string fileName = Guid.NewGuid() + file.FileName;
            string path = Path.Combine(root, folder, fileName);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
