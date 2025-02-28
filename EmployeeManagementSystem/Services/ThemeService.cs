using Microsoft.AspNetCore.Http;
using System;

namespace EmployeeManagementSystem.Services
{
    public class ThemeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string THEME_COOKIE_NAME = "preferred_theme";

        public ThemeService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentTheme()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.Request.Cookies.TryGetValue(THEME_COOKIE_NAME, out var theme))
            {
                return theme;
            }

            return "light"; // Default is light theme
        }

        public void SetTheme(string theme)
        {
            if (theme != "light" && theme != "dark")
            {
                throw new ArgumentException("Theme must be 'light' or 'dark'", nameof(theme));
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddYears(1),
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                HttpOnly = false // Allow JavaScript to read this cookie
            };

            httpContext.Response.Cookies.Append(THEME_COOKIE_NAME, theme, cookieOptions);
        }
    }
}