using LearnEase.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers
{
    [Route("[controller]")]
    public class AdminPageController : Controller
    {
        private readonly UserManager<User> userManager;

        public AdminPageController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}