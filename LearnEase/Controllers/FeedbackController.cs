using LearnEase.Models;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

[Route("[controller]")]
public class FeedbackController : Controller
{
    private readonly IFeedbackService feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        this.feedbackService = feedbackService;
    }

    [HttpGet("{courseId:int}")]
    public async Task<IActionResult> GetFeedbacks(int courseId) {
        try
        {
            var feedbacks = await this.feedbackService.GetAllFeedbacksByCourseIdAsync(courseId);
            return View(feedbacks);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Feedback newFeedback) {
        try {
            await this.feedbackService.CreateFeedbackAsync(newFeedback);
            return base.RedirectToAction(actionName: "GetFeedbacks", routeValues: new { courseId = feedbackService.CurrentCourseId });
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [Route("Edit/{feedbackId:int?}")]
    public async Task<IActionResult> GetFeedbackChangeMenu(int feedbackId) {
        var feedback = await feedbackService.GetFeedbackById(feedbackId);

        return View("FeedbackChangeMenu", feedback);
    }

    [HttpPost]
    [Route("[action]/{feedbackId:int}")]
    public async Task<IActionResult> Change(Feedback feedback, int feedbackId)
    {
        try
        {
            await this.feedbackService.PutFeedbackAsync(feedbackId, feedback);
            return base.RedirectToAction("GetFeedbacks", new { courseId = feedbackService.CurrentCourseId });
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{feedbackId:int}")]
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