using System.Text.Json;
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
            TempData["courseId"] = courseId;


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
            await this.feedbackService.CreateFeedbackAsync(newFeedback, (int)TempData["courseId"]);
            return base.RedirectToAction(actionName: "GetFeedbacks", routeValues: new { courseId = TempData["courseId"] });
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [Route("Edit/{feedbackId:int}")]
    public async Task<IActionResult> GetFeedbackChangeMenu(int feedbackId) {
        var feedback = await feedbackService.GetFeedbackById(feedbackId);

        return View("FeedbackChangeMenu", feedback);
    }

    [HttpPut("{feedbackId:int}")]
    public async Task<IActionResult> Change(int feedbackId,[FromBody] Feedback feedback)
    {
        try
        {
            // var streamReader = new StreamReader(Request.Body);
            // var requestBody = await streamReader.ReadToEndAsync();
            // var newFeedback = JsonSerializer.Deserialize<Feedback>(requestBody);

            await this.feedbackService.PutFeedbackAsync(feedbackId, feedback);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

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