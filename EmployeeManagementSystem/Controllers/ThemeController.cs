using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    public class ThemeController : Controller
    {
        private readonly ThemeService _themeService;

        public ThemeController(ThemeService themeService)
        {
            _themeService = themeService;
        }

        [HttpPost]
        public IActionResult Toggle(string returnUrl)
        {
            var currentTheme = _themeService.GetCurrentTheme();
            var newTheme = currentTheme == "light" ? "dark" : "light";

            _themeService.SetTheme(newTheme);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}