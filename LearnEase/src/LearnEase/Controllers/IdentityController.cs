using System.Security.Claims;
using FluentValidation;
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

    private readonly IUserRoleService userRoleService;

    private readonly IValidator<LoginDto> loginValidator;

    private readonly IValidator<RegistrationDto> registrationValidator;

    public IdentityController(IUserService userService, IValidator<LoginDto> loginValidator, 
        IValidator<RegistrationDto> registrationValidator, IUserRoleService userRoleService)
    {
        this.userService = userService;
        this.loginValidator = loginValidator;
        this.registrationValidator = registrationValidator;
        this.userRoleService = userRoleService;
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
            var validationResult = await loginValidator.ValidateAsync(loginDto);
            
            if (!validationResult.IsValid) {
                foreach(var error in validationResult.Errors)
                    base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return base.RedirectToRoute("LoginView", new
                {
                    loginDto.ReturnUrl
                });
            }

            var foundUser = await userService.FindUserByCredentialsAsync(loginDto);

            var claims = new List<Claim> {
                new("id", foundUser.Id.ToString()),
                new(ClaimTypes.Name, foundUser.Name),
                new(ClaimTypes.Email, foundUser.Email),
            };

            var roles = await userRoleService.GetUserRolesByUserId(foundUser.Id);

            foreach(var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role.Name));


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            if (string.IsNullOrWhiteSpace(loginDto.ReturnUrl) == false)
            {
                return base.Redirect(loginDto.ReturnUrl);
            }


            return base.RedirectToAction(controllerName: "Home", actionName: "Index");
        }
        catch
        {
            base.TempData["AuthenticationError"] = "Incorrect login or password!";

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
            var validationResult = await registrationValidator.ValidateAsync(registrationDto);
            
            if (!validationResult.IsValid) {
                foreach(var error in validationResult.Errors)
                    base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return base.View();
            }
            
            var registerAsAdmin = !await userService.HasAnyUsersRegistered();

            await userService.CreateUserAsync(registrationDto);
            var registratedUser = await userService.FindUserByCredentialsAsync(new LoginDto() 
                { 
                    Login = registrationDto.Name, 
                    Password = registrationDto.Password
                }
            );

            if (registerAsAdmin)
                await userRoleService.SetUserRoleAsync(registratedUser.Id, "Admin");

            else
                await userRoleService.SetDefaultUserRoleAsync(registratedUser.Id);

            return base.RedirectToRoute("LoginView");
        }
        catch (Exception)
        {
           return base.View();
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
