using Wayplot_Backend.Attributes;

namespace Wayplot_Backend.Middleware
{
    public sealed class RbacMiddleware
    {
        private readonly RequestDelegate _next;

        public RbacMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if endpoint requires role
            var endpoint = context.GetEndpoint();
            var roleAttr = endpoint?.Metadata.GetMetadata<RequiredRoleAttribute>();

            // No RBAC requirement → skip RBAC entirely
            if (roleAttr == null)
            {
                await _next(context);
                return;
            }

            var role = context.Items["Role"]?.ToString();

            if (role != roleAttr.Role)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    errorMessage = "Unauthorized: You do not have permission to view this route.",
                    message = $"Access Denied. Required Role: {roleAttr.Role}"
                });
                return;
            }

            await _next(context);
        }
    }

}
