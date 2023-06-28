using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contracts
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string path, string note = null);
        Task<string> UploadImageAsync(IFormFile file, string path, string note = null);
    }
}
