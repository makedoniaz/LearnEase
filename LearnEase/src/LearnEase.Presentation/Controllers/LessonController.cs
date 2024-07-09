using LearnEase.Core.Models;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers
{
    [Route("[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonService lessonService;

        private readonly UserManager<User> userManager;

        public LessonController(ILessonService lessonService, UserManager<User> userManager)
        {
            this.lessonService = lessonService;
            this.userManager = userManager;
        }


        [Authorize(Roles="Author, Admin")]
        [HttpPost("api/[action]", Name = "CreateLessonApi")]
        public async Task<IActionResult> Create([FromForm] Lesson lesson) {
            try {
                var authenticatedUser = await userManager.GetUserAsync(User);

                if (authenticatedUser is null)
                    return Forbid();

                await lessonService.CreateLessonAsync(authenticatedUser, lesson);

                return RedirectToAction(actionName: "Details", controllerName: "Course", new { id = lesson.CourseId });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles="Author, Admin")]
        [HttpGet("[action]", Name = "CreateLessonView")]
        public IActionResult Create(int courseId) {
            TempData["courseId"] = courseId;
            return View("CreateLesson");
        }


        [Authorize(Roles="Admin, Author")]
        [HttpDelete("{lessonId:int}")]
        public async Task<IActionResult> Delete(int lessonId)
        {
            try
            {
                var lesson = await lessonService.GetLessonByIdAsync(lessonId);

                var user = await userManager.GetUserAsync(User);
                var isAdmin = await userManager.IsInRoleAsync(user, "Admin");

                if (!isAdmin && lesson.UserId != user?.Id)
                    return Forbid();
                    
                await this.lessonService.DeleteLessonByIdAsync(lessonId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}