using System.Text.Json;
using LearnEase.Models;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService courseService;

    public CourseController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> Index()
    {
        var courses = await this.courseService.GetAllCoursesAsync();

        return View(courses);
    }

    [HttpPost]
    [Route("[controller]")]
    public async Task<IActionResult> CreateCourse(Course newCourse) {
        try {

            await this.courseService.CreateCourseAsync(newCourse);
            return base.RedirectToAction(actionName: "Index");
        }
        catch (Exception ex) {
            return BadRequest(ex);
        }
    }
}
