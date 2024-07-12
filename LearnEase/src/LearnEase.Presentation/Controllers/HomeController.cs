using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
