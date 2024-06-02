using System.Runtime.InteropServices;
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
    public async Task<IActionResult> Create([FromForm] Course newCourse, IFormFile logo) {
        try {
            var validationResult = await validator.ValidateAsync(newCourse);

            if (!validationResult.IsValid) {
                foreach(var error in validationResult.Errors)
                    base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                
                return base.View("CourseCreateMenu");
            }

            await this.courseService.SetCourseLogo(newCourse, logo);
            await this.courseService.CreateCourseAsync(newCourse);
            
            return base.RedirectToAction(actionName: "Index");
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
