using AssetManagement.Domain.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AssetManagement.API.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jwtSettings;
        private readonly ILogger<ResponseTimeMiddleware> _logger;

        public TokenValidationMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings, ILogger<ResponseTimeMiddleware> logger)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //if (!context.Request.Headers.ContainsKey("Authorization"))
            //{
            //    context.Response.StatusCode = 401;
            //    await context.Response.WriteAsync("Unauthorized request");
            //    _logger.LogInformation("Unauthorized request");

            //    return;
            //}
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!IsValidToken(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized request");
                _logger.LogInformation("Unauthorized request");
                return;
            }
            await _next(context);
        }

        private bool IsValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}