
using Business_Layer.ServiceRepository.ErrorLogingService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace Business_Layer.ServiceRepository.StorageService;

public class UploadImage : IUploadImge
{
    private string? _imagePath;
    private IFileLogService _log;

    public UploadImage(IFileLogService log)
    {
        _log = log;
    }
    public async Task<string>? GetImagePath()
    {
        return await Task.Run(() => _imagePath);
    }

    public async Task UploadImageService(IFormFile file)
    {
        
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Gallery");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        try
        {
            
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

           
            var filePath = Path.Combine(folderPath, uniqueFileName);

      
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            var imagePath = Path.Combine(@"\Gallery", uniqueFileName);
            _imagePath = imagePath;
        }
        catch (IOException ex)
        {
            
            await _log.LogErrorTextFile($"Time:{DateTime.Now} Storage Service Exception: {ex}");
        }
    }


}
