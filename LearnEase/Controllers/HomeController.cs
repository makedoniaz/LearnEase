using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
