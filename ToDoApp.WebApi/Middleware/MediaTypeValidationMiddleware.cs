namespace ToDoApp.WebApi.Middleware;

public class MediaTypeValidationMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly string[] SupportedMediaTypes = { "application/json", "application/xml", "text/plain" };

    public MediaTypeValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Options)
        {
            await _next(context);
            return;
        }
        
        if (!string.IsNullOrEmpty(context.Request.ContentType) &&
            !SupportedMediaTypes.Contains(context.Request.ContentType))
        {
            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                message = "The Content-Type is not supported.",
                supportedTypes = SupportedMediaTypes
            });
            return;
        }
        
        var acceptHeader = context.Request.Headers["Accept"].ToString();
        if (!string.IsNullOrEmpty(acceptHeader))
        {
            var acceptTypes = acceptHeader.Split(',').Select(type => type.Trim());
            if (!acceptTypes.Any(type => SupportedMediaTypes.Contains(type)))
            {
                context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "The Accept header is not supported.",
                    supportedTypes = SupportedMediaTypes
                });
                return;
            }
        }

        await _next(context);
    }
}