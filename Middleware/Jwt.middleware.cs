using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wayplot_Backend.Attributes;

namespace Wayplot_Backend.Middleware
{
    public sealed class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if endpoint requires role
            var endpoint = context.GetEndpoint();
            var roleAttribute = endpoint?.Metadata.GetMetadata<RequiredRoleAttribute>();

            // No RBAC requirement → skip JWT entirely
            if (roleAttribute == null)
            {
                await _next(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    errorMessage = "JWT Token Missing. Please login. [1]",
                    message = "JWT token missing"
                });
                return;
            }

            var token = authHeader["Bearer ".Length..].Trim();

            Console.WriteLine("------------------");
            var rawToken = token; // string without "Bearer "
            var jwt = new JwtSecurityTokenHandler
            {
                MapInboundClaims = false
            }.ReadJwtToken(rawToken);
            var payload = jwt.Payload
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            Console.WriteLine(
                System.Text.Json.JsonSerializer.Serialize(
                    payload,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                )
            );


            try {
                var keyString = _config["JwtSettings:Key"];
                if (string.IsNullOrWhiteSpace(keyString)) throw new Exception("JWT key not configured");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidAudience = _config["JwtSettings:Audience"],
                    IssuerSigningKey = key,

                    ClockSkew = TimeSpan.Zero
                };

                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(token, validationParams, out _);
                var decoded = principal.Claims
                .GroupBy(c => c.Type)
                .ToDictionary(g => g.Key, g => g.Select(c => c.Value).ToArray());

                Console.WriteLine("---------------------------------");
                Console.WriteLine(
                    System.Text.Json.JsonSerializer.Serialize(
                        decoded,
                        new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                    )
                );
                Console.WriteLine("---------------------------------");
                Console.WriteLine(ClaimTypes.NameIdentifier);
                Console.WriteLine(ClaimTypes.Email);
                Console.WriteLine(ClaimTypes.Role);


                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = principal.FindFirst(ClaimTypes.Role)?.Value;
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;

                if (userId == null || role == null || email == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        success = false,
                        message = "Required JWT claims missing. Need User ID, Role and Email [1]"
                    });
                    return;
                }

                context.Items["UserId"] = userId;
                context.Items["Role"] = role;
                context.Items["Email"] = email;
            }
            catch (Exception err)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = "Invalid or expired JWT token [1]",
                    errorMessage = err.Message,
                    token
                });
                return;
            }

            await _next(context);
        }
    }
}
