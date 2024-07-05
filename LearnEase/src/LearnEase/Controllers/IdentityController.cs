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
    private readonly IValidator<LoginDto> loginValidator;

    private readonly IValidator<RegistrationDto> registrationValidator;

    public IdentityController(IValidator<LoginDto> loginValidator, IValidator<RegistrationDto> registrationValidator)
    {
        this.loginValidator = loginValidator;
        this.registrationValidator = registrationValidator;
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
