using Application.Services.Contracts;
using Domain.Utils;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Implementations
{
    public class FileUploadService : IFileUploadService
    {
        private IUriService _uriService;

        public FileUploadService(IUriService uriService)
        {
            _uriService = uriService;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder, string note = null)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new Exception("No file provided");
                }

                var baseUrl = _uriService.GetBaseUri();
                var uniqueFileName = FormatUtil.GenerateUniqueFileName(file.FileName);
                var url = $"{baseUrl}Upload/d/{folder}/{uniqueFileName}";

                // Get the full path to the wwwroot directory
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", "d", folder);
                string filePath = Path.Combine(folderPath, uniqueFileName);


                // Check if the target folder exists
                if (!Directory.Exists(folderPath))
                {
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(folderPath);
                }

                // Check if the target file already exists
                while (File.Exists(filePath))
                {
                    // Generate a new unique file name if the file already exists
                    uniqueFileName = FormatUtil.GenerateUniqueFileName(file.FileName);
                    filePath = Path.Combine(folderPath, uniqueFileName);
                }

                // Open the file stream
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Copy the file data to the wwwroot directory
                    await file.OpenReadStream().CopyToAsync(fileStream);
                }
                return url;
            }
            catch (Exception ex)
            {
                ex.Source = $"FileUploadService.UploadFileAsync";
                throw;
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder, string note = null)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new Exception("No file provided");
                }

                var baseUrl = _uriService.GetBaseUri();
                var uniqueFileName = FormatUtil.GenerateUniqueFileName(file.FileName);
                var url = $"{baseUrl}Upload/v/{folder}/{uniqueFileName}";

                // Get the full path to the wwwroot directory
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", "v", folder);
                string filePath = Path.Combine(folderPath, uniqueFileName);


                // Check if the target folder exists
                if (!Directory.Exists(folderPath))
                {
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(folderPath);
                }

                // Check if the target file already exists
                while (File.Exists(filePath))
                {
                    // Generate a new unique file name if the file already exists
                    uniqueFileName = FormatUtil.GenerateUniqueFileName(file.FileName);
                    filePath = Path.Combine(folderPath, uniqueFileName);
                }

                // Validate file type
                if (!IsValidImageType(file.ContentType))
                {
                    throw new Exception("Invalid file type. Only JPEG and PNG are allowed.");
                }

                // Open the file stream
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Copy the file data to the wwwroot directory
                    await file.OpenReadStream().CopyToAsync(fileStream);
                }
                return url;
            }
            catch (Exception ex)
            {
                ex.Source = $"FileUploadService.UploadFileAsync";
                throw;
            }
        }
        private bool IsValidImageType(string contentType)
        {
            // Allow only JPEG and PNG
            return new[] { "image/jpeg", "image/png" }.Contains(contentType);
        }

    }
}
