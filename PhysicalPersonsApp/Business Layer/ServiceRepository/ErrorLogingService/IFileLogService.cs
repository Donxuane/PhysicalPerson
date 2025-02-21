namespace Business_Layer.ServiceRepository.ErrorLogingService;

public interface IFileLogService
{
    public Task LogErrorTextFile(string content);
}
