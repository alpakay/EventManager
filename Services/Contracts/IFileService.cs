using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string rootPath);
        void DeleteFile(string fileName, string rootPath);
    }
}