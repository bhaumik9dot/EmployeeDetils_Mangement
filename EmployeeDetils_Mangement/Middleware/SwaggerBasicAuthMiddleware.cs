using System.Net;
using System.Text;

namespace EmployeeDetils_Mangement.Middleware
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerBasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic"))
                {
                    var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                    var credentialBytes = Convert.FromBase64String(encodedCredentials);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                    if (credentials.Length == 2)
                    {
                        var username = credentials[0];
                        var password = credentials[1];

                        if (username == "admin" && password == "password") // Secure this in production
                        {
                            await _next(context);
                            return;
                        }
                    }
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
        }
    }
}
