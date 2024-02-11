namespace GamesApi.WebApi.Middleware
{
    public class ExceptionLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionLoggingMiddleware> _logger;

        public ExceptionLoggingMiddleware(ILogger<ExceptionLoggingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                throw; // Re-throw the exception to preserve the original behavior
            }
        }
    }
}
