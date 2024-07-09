using LearnEase.Core.Models;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers
{
    [Route("[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonService lessonService;

        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }


        [Authorize(Roles="Author, Admin")]
        [HttpPost("api/[action]", Name = "CreateLessonApi")]
        public async Task<IActionResult> Create([FromForm] Lesson lesson) {
            try {
                await lessonService.CreateLessonAsync(lesson);

                return RedirectToAction(actionName: "GetCourseDetails", controllerName: "Courses", new { id = lesson.CourseId });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles="Author, Admin")]
        [HttpGet("[action]", Name = "GetCreateLessonView")]
        public IActionResult Create() {
            return View("CreateLesson");
        }
    }
}