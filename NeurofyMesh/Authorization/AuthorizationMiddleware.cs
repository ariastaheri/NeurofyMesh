namespace NeurofyMesh.Authorization
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("x-api-key"))
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("User must provide correct headers!");
                return;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
