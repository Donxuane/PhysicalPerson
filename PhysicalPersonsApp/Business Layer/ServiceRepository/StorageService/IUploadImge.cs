using Microsoft.AspNetCore.Http;

namespace Business_Layer.ServiceRepository.StorageService;

public interface IUploadImge
{
    public Task UploadImageService(IFormFile file);
    public Task<string>? GetImagePath();
}
