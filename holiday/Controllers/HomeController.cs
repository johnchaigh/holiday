using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using holiday.Models;
using Microsoft.AspNetCore.Identity;
using holiday.Data;
using System.Security.Claims;

namespace holiday.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ViewBag.Id = Id;
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

