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

    [HttpGet("{courseId:int}")]
    public async Task<IActionResult> GetFeedbacks(int courseId) {
        try
        {
            var feedbacks = await this.feedbackService.GetAllFeedbacksByCourseIdAsync(courseId);
            TempData["courseId"] = courseId;

            return View(feedbacks);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "NotMuted")]
    [Authorize(Roles="User, Author, Admin")]
    [HttpGet("[action]", Name = "CreateFeedbackView")]
    public IActionResult Create() {
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
            return base.RedirectToAction(actionName: "GetFeedbacks", routeValues: new { courseId = newFeedback.CourseId });
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [Route("Edit/{feedbackId:int}")]
    public async Task<IActionResult> GetFeedbackChangeMenu(int feedbackId) {
        var feedback = await feedbackService.GetFeedbackById(feedbackId);

        return base.View("FeedbackChangeMenu", feedback);
    }

    [Authorize(Roles="Admin")]
    [HttpPut("{feedbackId:int}")]
    public async Task<IActionResult> Change(int feedbackId, [FromBody] Feedback feedback)
    {
        try
        {
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

    [Authorize(Roles="Admin")]
    [HttpDelete("{feedbackId:int}")]
    public async Task<IActionResult> Delete(int feedbackId)
    {
        try
        {
            await this.feedbackService.DeleteFeedbackAsync(feedbackId);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}