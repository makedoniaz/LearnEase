using FluentValidation;

using LearnEase.Core.Models;
using LearnEase.Core.Services;
using LearnEase.Presentation.Utilities.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers;

[Route("[controller]")]
public class FeedbackController : Controller
{
    private readonly IFeedbackService feedbackService;

    private readonly UserManager<User> userManager;

    private readonly IValidator<Feedback> validator;

    public FeedbackController(IFeedbackService feedbackService, IValidator<Feedback> validator, UserManager<User> userManager)
    {
        this.feedbackService = feedbackService;
        this.validator = validator;
        this.userManager = userManager;
    }

    [Authorize(Policy = "NotMuted")]
    [Authorize(Roles="User, Author, Admin")]
    [HttpGet("[action]", Name = "CreateFeedbackView")]
    public IActionResult Create(int courseId) {
        TempData["courseId"] = courseId;
        return View("FeedbackCreateMenu");
    }

    [Authorize(Policy = "NotMuted")]
    [Authorize(Roles="User, Author, Admin")]
    [HttpPost(Name = "CreateFeedbackApi")]
    public async Task<IActionResult> Create([FromForm] Feedback newFeedback) {
        try {
            var validationResult = await validator.ValidateAsync(newFeedback);

            var errorHandlerResult = this.HandleValidationErrors(
                validationResult, 
                returnLogic: () => base.View("FeedbackCreateMenu"),
                pageKey: "FeedbackCreatePage"
            );
            
            if (errorHandlerResult != null)
                return errorHandlerResult;

            var authenticatedUser = await userManager.GetUserAsync(User);

            await this.feedbackService.CreateFeedbackAsync(authenticatedUser, newFeedback);
            return base.RedirectToAction(controllerName: "Course", actionName: "Details", routeValues: new { id = newFeedback.CourseId });
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles="Author, Admin")]
    [Route("Edit/{feedbackId:int}")]
    public async Task<IActionResult> GetFeedbackChangeMenu(int feedbackId) {

        var feedback = await feedbackService.GetFeedbackById(feedbackId);
        var user = await userManager.GetUserAsync(User);
        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");

        if (!isAdmin && feedback.UserId != user?.Id)
            return Forbid();

        return base.View("FeedbackChangeMenu", feedback);
    }

    [Authorize(Roles="Admin, Author")]
    [HttpPut("{feedbackId:int}")]
    public async Task<IActionResult> Change(int feedbackId, [FromBody] Feedback feedback)
    {
        try
        {
            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin && feedback.UserId != user?.Id)
                return Forbid();

            var validationResult = await validator.ValidateAsync(feedback);

            var errorHandlerResult = this.HandleValidationErrors(
                validationResult, 
                returnLogic: () => base.View("FeedbackChangeMenu"),
                pageKey: "FeedbackChangePage"
            );
            
            if (errorHandlerResult != null)
                return errorHandlerResult;


            await this.feedbackService.PutFeedbackAsync(feedbackId, feedback);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles="Admin, Author")]
    [HttpDelete("{feedbackId:int}")]
    public async Task<IActionResult> Delete(int feedbackId)
    {
        try
        {
            var feedback = await feedbackService.GetFeedbackById(feedbackId);

            if (feedback == null)
                return NotFound();

            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin && feedback.UserId != user?.Id)
                return Forbid();

            await this.feedbackService.DeleteFeedbackAsync(feedbackId);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}