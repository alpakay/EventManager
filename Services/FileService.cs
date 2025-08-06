using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services.Contracts;

namespace Services;

public class FileService : IFileService
{
    public async Task<string> UploadFileAsync(IFormFile file, string rootPath)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Dosya boş olamaz");
        }
        if (file.Length > 2 * 1024 * 1024) // 2MB
        {
            throw new ArgumentException("Dosya boyutu 2 MB'ı aşamaz.");
        }
        var extension = Path.GetExtension(file.FileName).ToLower();
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        if (!allowedExtensions.Contains(extension))
        {
            throw new ArgumentException("Geçersiz dosya türü. Geçerli türler: " + string.Join(", ", allowedExtensions));
        }
        var fileName = Guid.NewGuid() + extension;
        var filePath = Path.Combine(rootPath, "uploads", fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return fileName;
    }

    public void DeleteFile(string fileName, string rootPath)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("Dosya adı boş olamaz.");
        }
        var filePath = Path.Combine(rootPath, "uploads", fileName);
        if (!File.Exists(filePath))
        {
            return;
        }

        File.Delete(filePath);
        return;
    }
}