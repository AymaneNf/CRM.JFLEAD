namespace CRM.AuthAPI.Middleware
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var requiredPermissions = endpoint.Metadata.GetMetadata<IList<string>>();
                if (requiredPermissions != null)
                {
                    foreach (var permission in requiredPermissions)
                    {
                        if (!context.User.HasClaim("Permission", permission))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
