using System.Text.Json;
using LearnEase.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

public class CourseController : Controller
{

    [HttpGet]
    [Route("[controller]")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("[controller]")]
    public async Task<IActionResult> CreateCourse(Course newCourse) {

        if (string.IsNullOrWhiteSpace(newCourse.Name) || string.IsNullOrWhiteSpace(newCourse.Description))
            return BadRequest();

        newCourse.CreationDate = DateTime.Now;

        var coursesJson = await System.IO.File.ReadAllTextAsync("Assets/courses.json");

        var courses = JsonSerializer.Deserialize<List<Course>>(coursesJson, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        });

        courses ??= new List<Course>();
        courses.Add(newCourse);

        var newCoursesJson = JsonSerializer.Serialize(courses, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        });

        await System.IO.File.WriteAllTextAsync("Assets/courses.json", newCoursesJson);

        return base.RedirectToAction(actionName: "Index");
    }
}
