using LearnEase.Core.Models;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnEase.Presentation.Controllers;

[Authorize(Roles = "Admin")]
public class AdminPageController : Controller
{
    private readonly IAdminPageService adminPageService;

    public AdminPageController(IAdminPageService adminPageService)
    {
        this.adminPageService = adminPageService;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("[controller]/[action]/{userId}", Name = "ToggleMute")]
    public async Task<IActionResult> ToggleMuteUser(string userId) {
        try {
            await adminPageService.ToggleMuteUser(userId);
        }
        catch (Exception ex) {
            BadRequest(ex.Message);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("[controller]/[action]/{userId}", Name = "TogglePromoteToAdmin")]
    public async Task<IActionResult> TogglePromoteToAdmin(string userId) {
        try {
            await adminPageService.TogglePromoteUserToAdmin(userId);
        }
        catch (Exception ex) {
            BadRequest(ex.Message);
        }

        return RedirectToAction("Index");
    }


    [HttpPost]
    [Route("[controller]/[action]/{userId}", Name = "TogglePromoteToAuthor")]
    public async Task<IActionResult> TogglePromoteToAuthor(string userId) {
        try {
            await adminPageService.TogglePromoteUserToAuthor(userId);
        }
        catch (Exception ex) {
            BadRequest(ex.Message);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("[controller]/[action]/{userId}", Name = "ToggleBanUser")]
    public async Task<IActionResult> ToggleBanUser(string userId) {
        try {
            await adminPageService.ToggleBanUser(userId);
        }
        catch (Exception ex) {
            BadRequest(ex.Message);
        }

        return RedirectToAction("Index");
    }
}
