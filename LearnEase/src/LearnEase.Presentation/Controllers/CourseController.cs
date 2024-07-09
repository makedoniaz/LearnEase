using FluentValidation;

using LearnEase.Core.Models;
using LearnEase.Core.Services;
using LearnEase.Presentation.Utilities.Extensions;
using LearnEase.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Presentation.Controllers;

[Route("[controller]")]
public class CourseController : Controller
{
    private readonly ICourseService courseService;

    private readonly IFeedbackService feedbackService;

    private readonly IValidator<Course> validator;

    private readonly UserManager<User> userManager;


    public CourseController(ICourseService courseService, IValidator<Course> validator, 
    IFeedbackService feedbackService, UserManager<User> userManager)
    {
        this.courseService = courseService;
        this.validator = validator;
        this.feedbackService = feedbackService;
        this.userManager = userManager;
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
    

    [Authorize(Roles="Admin, Author")]
    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> Delete(int courseId) {
        try
        {
            var course = await courseService.GetCourseByIdAsync(courseId);

            if (course == null)
                return NotFound();

            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin && course.UserId != user?.Id)
                return Forbid();


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


    [Authorize(Roles="User, Author, Admin")]
    [HttpGet("[action]", Name = "GetCourseDetails")]
    public async Task<IActionResult> Details(int id) {
        try {
            var course = await courseService.GetCourseByIdAsync(id);

            var viewModel = new CourseDetailsViewModel
            {
                Course = course,
                Lessons = course.Lessons,
                Feedbacks = course.Feedbacks
            };

            return View("CoursePage", viewModel);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
