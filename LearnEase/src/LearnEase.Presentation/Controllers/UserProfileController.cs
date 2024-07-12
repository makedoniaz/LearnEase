using LearnEase.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace LearnEase.Presentation.Controllers;

[Route("[controller]")]
public class UserProfileController : Controller
{
    private readonly UserManager<User> userManager;

    public UserProfileController(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
        return View(user);
    }
}
