namespace WebApp.CustomMiddleware;

public class ResponseHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.OnStarting(state =>
        {
            var httpContext = (HttpContext)state;
            httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            return Task.CompletedTask;
        }, context);

        await _next(context);
    }
}