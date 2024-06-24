using System.Security.Claims;
using LearnEase.Dtos;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

[Route("[controller]")]
public class IdentityController : Controller
{
    private readonly IUserService userService;

    public IdentityController(IUserService userService)
    {
        this.userService = userService;
    }


    [Route("/[controller]/[action]", Name = "LoginView")]
    public IActionResult Login(string? ReturnUrl)
    {
        ViewBag.ReturnUrl = ReturnUrl;
        return base.View();
    }


    [HttpPost]
    [Route("/api/[controller]/[action]", Name = "LoginEndpoint")]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {
        try 
        {
            var foundUser = await userService.FindUserByCredentialsAsync(loginDto);

            var claims = new Claim[] {
                new("id", foundUser.Id.ToString()),
                new(ClaimTypes.Name, foundUser.Name),
                new(ClaimTypes.Email, foundUser.Email),
                new(ClaimTypes.Role, "test"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return base.RedirectToAction(controllerName: "Home", actionName: "Index");
        }
        catch
        {
            return base.RedirectToRoute("LoginView", new
            {
                loginDto.ReturnUrl
            });

        }
    }

    [Route("/[controller]/[action]", Name = "RegistrationView")]
    public IActionResult Registration()
    {
        return base.View();
    }

    [HttpPost]
    [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
    public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto)
    {
        try
        {
            await userService.CreateUserAsync(registrationDto);
            return base.RedirectToRoute("LoginView");
        }
        catch
        {
           return BadRequest();
        }
    }

    [HttpGet]
    [Route("/api/[controller]/[action]")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
    {
        await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return base.RedirectToRoute(routeName: "LoginView", routeValues: new
        {
            ReturnUrl
        });
    }
}
