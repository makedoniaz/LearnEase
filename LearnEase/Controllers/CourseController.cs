using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

[Route("[controller]")]
public class CourseController : Controller
{

    [HttpGet]
    [Route("[controller]")]
    public IActionResult Index()
    {
        return View();
    }
}
