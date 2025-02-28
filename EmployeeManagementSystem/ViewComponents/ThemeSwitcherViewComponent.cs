using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Services;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewComponents
{
    public class ThemeSwitcherViewComponent : ViewComponent
    {
        private readonly ThemeService _themeService;

        public ThemeSwitcherViewComponent(ThemeService themeService)
        {
            _themeService = themeService;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            var currentTheme = _themeService.GetCurrentTheme();
            // Specificera "Default" som vy-namn här
            return Task.FromResult<IViewComponentResult>(View("Default", currentTheme));
        }
    }
}