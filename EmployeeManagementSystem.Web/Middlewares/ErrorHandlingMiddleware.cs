using NLog;

namespace EmployeeManagementSystem.Web.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Log(NLog.LogLevel.Error, ex);
            throw;
        }
    }
}