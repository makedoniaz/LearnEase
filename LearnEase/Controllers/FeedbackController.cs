using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Logging;

namespace LearnEase.Controllers
{
    [Route("[controller]")]
    public class FeedbackController : Controller
    {

        IFeedbackService feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        [HttpGet("{courseId?}")]
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
                return base.RedirectToAction(actionName: "GetFeedbacks", routeValues: new { courseId = newFeedback.CourseId });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Change(Feedback feedback)
        {
            try
            {
                await this.feedbackService.PutFeedbackAsync(feedback.Id, feedback);
                return base.RedirectToAction(actionName: "Index");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int feedbackId)
        {
            try
            {
                await this.feedbackService.DeleteFeedbackAsync(feedbackId);
                return base.RedirectToAction(actionName: "Index");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}