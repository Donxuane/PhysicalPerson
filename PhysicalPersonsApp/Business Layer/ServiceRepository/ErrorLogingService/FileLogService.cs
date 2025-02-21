namespace Business_Layer.ServiceRepository.ErrorLogingService;

public class FileLogService : IFileLogService
{
    private static readonly SemaphoreSlim _lock = new(1, 1);
    public async Task LogErrorTextFile(string content)
    {

        string filePath = "wwwroot/LogService/Error.txt";
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        await _lock.WaitAsync();
        try
        {
            using (var writer = new StreamWriter(filePath, append: true))
            {
                await writer.WriteLineAsync(content);
            }
        }
        finally
        {
            _lock.Release();
        }
    }
}
