using LearnEase.Models;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

[Route("[controller]")]
public class CourseController : Controller
{
    private readonly ICourseService courseService;

    public CourseController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var courses = await this.courseService.GetAllCoursesAsync();

        return View(courses);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(Course newCourse) {
        try {
            await this.courseService.CreateCourseAsync(newCourse);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
