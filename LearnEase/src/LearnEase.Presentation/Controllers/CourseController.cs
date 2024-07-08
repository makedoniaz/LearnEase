using FluentValidation;

using LearnEase.Core.Models;
using LearnEase.Core.Services;
using LearnEase.Presentation.Utilities.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers;

[Route("[controller]")]
public class CourseController : Controller
{
    private readonly ICourseService courseService;

    private readonly IFeedbackService feedbackService;

    private readonly IValidator<Course> validator;


    public CourseController(ICourseService courseService, IValidator<Course> validator, IFeedbackService feedbackService)
    {
        this.courseService = courseService;
        this.validator = validator;
        this.feedbackService = feedbackService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var courses = await this.courseService.GetAllCoursesAsync();
        return View(courses);
    }


    [Authorize(Roles="Author, Admin")]
    [HttpGet("[action]", Name = "CourseCreateView")]
    public IActionResult Create() {
        return base.View("CourseCreateMenu");
    }
    

    [Authorize(Roles="Author, Admin")]
    [HttpPost(Name = "CourseCreateApi")]
    public async Task<IActionResult> Create([FromForm] Course newCourse, IFormFile? logo) {
        try {
            var validationResult = await validator.ValidateAsync(newCourse);

            var errorHandlerResult = this.HandleValidationErrors(
                validationResult, 
                returnLogic: () => base.View("CourseCreateMenu"),
                pageKey: "CourseCreatePage"
            );
            
            if (errorHandlerResult != null)
                return errorHandlerResult;
                

            await this.courseService.CreateCourseAsync(newCourse);
            await this.courseService.SetCourseLogo(newCourse, logo);
            
            return base.RedirectToAction(actionName: "Index");
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
    

    [Authorize(Roles="Admin")]
    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> Delete(int courseId) {
        try
        {
            await this.feedbackService.DeleteFeedbacksByCourseId(courseId);
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
