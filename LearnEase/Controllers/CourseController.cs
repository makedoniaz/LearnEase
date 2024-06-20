using FluentValidation;
using LearnEase.Models;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Controllers;

[Route("[controller]")]
public class CourseController : Controller
{
    private readonly ICourseService courseService;

    private readonly IValidator<Course> validator;

    public CourseController(ICourseService courseService, IValidator<Course> validator)
    {
        this.courseService = courseService;
        this.validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var courses = await this.courseService.GetAllCoursesAsync();
        return View(courses);
    }


    [HttpGet("[action]", Name = "CourseCreateView")]
    public IActionResult Create() {
        return base.View("CourseCreateMenu");
    }
    

    [HttpPost(Name = "CourseCreateApi")]
    public async Task<IActionResult> Create([FromForm] Course newCourse, IFormFile? logo) {
        try {
            var validationResult = await validator.ValidateAsync(newCourse);

            if (!validationResult.IsValid) {
                foreach(var error in validationResult.Errors)
                    base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                
                return base.View("CourseCreateMenu");
            }

            await this.courseService.CreateCourseAsync(newCourse);
            await this.courseService.SetCourseLogo(newCourse, logo);
            
            return base.RedirectToAction(actionName: "Index");
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> Delete(int courseId) {
        try
        {
            await this.courseService.DeleteCourseLogo(courseId);
            await this.courseService.DeleteCourseByIdAsync(courseId);

            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Logo(int id) {
        var courses = await courseService.GetAllCoursesAsync();
        var course = courses.FirstOrDefault(c => c.Id == id);

        if (course is null)
            return BadRequest();

        if (course.CourseLogoPath is null)
            return Ok();

        var fileStream = System.IO.File.Open(course.CourseLogoPath, FileMode.Open);
        return base.File(fileStream, "image/jpeg");
    }
}
