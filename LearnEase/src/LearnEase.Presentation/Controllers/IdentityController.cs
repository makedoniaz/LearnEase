using FluentValidation;

using LearnEase.Core.Dtos;
using LearnEase.Core.Services;
using LearnEase.Presentation.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers;

public class IdentityController : Controller
{
    private readonly IValidator<LoginDto> loginValidator;

    private readonly IValidator<RegistrationDto> registrationValidator;

    private readonly IIdentityService identityService; 


    public IdentityController(IIdentityService identityService, IValidator<LoginDto> loginValidator, 
    IValidator<RegistrationDto> registrationValidator)
    {
        this.identityService = identityService;
        this.loginValidator = loginValidator;
        this.registrationValidator = registrationValidator;
    }


    [Route("/[controller]/[action]", Name = "LoginView")]
    public IActionResult Login(string? ReturnUrl)
    {
        this.RestoreValidationErrors("LoginPage");
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

            var errorHandlerResult = this.HandleValidationErrors(
                validationResult, 
                returnLogic: () => base.RedirectToRoute("LoginView", new { loginDto.ReturnUrl }),
                pageKey: "LoginPage"
            );
            
            if (errorHandlerResult != null)
                return errorHandlerResult;

            await identityService.SignInAsync(loginDto);

            if (string.IsNullOrWhiteSpace(loginDto.ReturnUrl) == false)
                return base.Redirect(loginDto.ReturnUrl);

            return base.RedirectToAction(controllerName: "Home", actionName: "Index");
        }
        catch (Exception ex)
        {
            base.TempData["AuthenticationError"] = ex.Message;

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
            
            var errorHandlerResult = this.HandleValidationErrors(
                validationResult, 
                returnLogic: () => base.View(),
                pageKey: "RegisterPage"
            );

            if (errorHandlerResult != null)
                return errorHandlerResult;

            await identityService.RegisterAsync(registrationDto);

            return base.RedirectToRoute("LoginView");
        }
        catch (Exception ex)
        {
            this.ModelState.AddModelError("Password", ex.Message);
            return base.View();
        }
    }

    [HttpGet]
    [Route("/api/[controller]/[action]")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
    {
        await identityService.SignOutAsync();

        return base.RedirectToRoute(routeName: "LoginView", routeValues: new
        {
            ReturnUrl
        });
    }
}
