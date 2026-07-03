using _12.Models;
using Microsoft.AspNetCore.Mvc;

namespace _12.Controllers;

public class TimelineController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Timeline";
        return View(TimelineData.Entries);
    }
}
