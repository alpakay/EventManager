using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagerApp.Areas.Management.Controllers
{
    public abstract class BaseController : Controller
    {
        // User Information Properties
        protected int CurrentUserId => GetCurrentUserId();
        protected string CurrentUserEmail => User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        protected string CurrentUserName => User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        protected bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

        // Helper Methods
        private int GetCurrentUserId()
        {
            if (!IsAuthenticated)
                return 0;

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }

        // Common Actions
        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "User", new { area = "Management" });
        }

        protected IActionResult HandleNotFound(string message = "Resource not found")
        {
            return NotFound(message);
        }

        protected IActionResult HandleUnauthorized(string message = "Unauthorized access")
        {
            return Forbid(message);
        }

        // Success/Error Messages
        protected void AddSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        protected void AddErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }
    }
}