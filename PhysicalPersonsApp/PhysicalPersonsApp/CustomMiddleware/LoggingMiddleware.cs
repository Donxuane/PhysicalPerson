
using Business_Layer.ServiceRepository.ErrorLogingService;

namespace PhysicalPersonsApp.CustomMiddleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private IFileLogService _log;

        public LoggingMiddleware(RequestDelegate next, IFileLogService log)
        {
            _next = next;
            _log = log; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode.Equals(400) || context.Response.StatusCode.Equals(499))
                {
                    await _log.LogErrorTextFile($"Client Error Response Request path: {context.Request.Path}/-Error");
                }
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Request path: {context.Request.Path}//\tError: {ex}");
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Error Occured While Processing Request");
            }
        }
    }
}
