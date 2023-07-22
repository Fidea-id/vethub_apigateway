using Microsoft.AspNetCore.Http;

namespace Application.Services.Contracts
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string path, string note = null);
        Task<string> UploadImageAsync(IFormFile file, string path, string note = null);
    }
}
