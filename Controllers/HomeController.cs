using _12.Models;
using _12.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _12.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiscordNotificationService _discordNotificationService;

        public HomeController(DiscordNotificationService discordNotificationService)
        {
            _discordNotificationService = discordNotificationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFound404()
        {
            var path = HttpContext.Features.Get<IStatusCodeReExecuteFeature>()?.OriginalPath ?? "/";

            ViewData["Title"] = "404 - Page Not Found";
            return View((object)path);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(string name, string email, string message)
        {
            name = name?.Trim() ?? string.Empty;
            email = email?.Trim() ?? string.Empty;
            message = message?.Trim() ?? string.Empty;

            var wordCount = message.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;

            if (string.IsNullOrWhiteSpace(name) || name.Length > 100
                || string.IsNullOrWhiteSpace(email) || email.Length > 254 || !IsValidEmail(email)
                || string.IsNullOrWhiteSpace(message) || wordCount > 500)
            {
                TempData["ContactMessage"] = "Please check your details: enter a valid name and email, and keep your message to 500 words or fewer.";
                return Redirect("/#contact");
            }

            var sent = await _discordNotificationService.SendContactMessageAsync(name, email, message);

            TempData["ContactMessage"] = sent
                ? "Thanks for reaching out! I'll get back to you within 48 hours."
                : "Thanks for reaching out! Something went wrong delivering your message — please email peter@husky.nz directly in the meantime.";

            return Redirect("/#contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
