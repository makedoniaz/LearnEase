using LearnEase.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace LearnEase.Presentation.Controllers;

[Authorize(Roles = "Admin")]
public class AdminPageController : Controller
{
    private readonly UserManager<User> userManager;

    public AdminPageController(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("[controller]/[action]/{userId}", Name = "ToggleMute")]
    public async Task<IActionResult> ToggleMuteUser(string userId) {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return BadRequest();

        user.IsMuted = !user.IsMuted;
        await userManager.UpdateAsync(user);


        var claims = await userManager.GetClaimsAsync(user);
        var isMutedClaim = claims.FirstOrDefault(c => c.Type == "IsMuted");

        if (isMutedClaim == null)
            throw new Exception("Muted claim doesn't exist!");
            
        await userManager.RemoveClaimAsync(user, isMutedClaim);
        await userManager.AddClaimAsync(user, new Claim("IsMuted", user.IsMuted.ToString()));


        claims = await userManager.GetClaimsAsync(user);
        foreach (var claim in claims)
            Console.WriteLine($"Claim: {claim.Type} {claim.Value}");

        return RedirectToAction("Index");
    }
}
