using Microsoft.Extensions.Options;
using StudentChat.BLL.Configuration;

namespace StudentChat.WebApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static void AppendTokenToCookie(this HttpContext context, string token, IOptions<JwtConfiguration> options)
        {
            var _jwtConfiguration = options.Value;

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None, // Required for cross-domain cookies
                Domain = ".localhost", // Allows cookies for all subdomains
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessTokenLifetime)
            };

            context.Response.Cookies.Append("AuthToken", token, cookieOptions);
        }

        public static void AppendRefreshTokenToCookie(this HttpContext context, string token, IOptions<JwtConfiguration> options)
        {

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var _jwtConfiguration = options.Value;

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None, // Required for cross-domain cookies
                Domain = ".localhost", // Allows cookies for all subdomains
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.RefreshTokenLifetime)
            };

            context.Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }
    }
}
