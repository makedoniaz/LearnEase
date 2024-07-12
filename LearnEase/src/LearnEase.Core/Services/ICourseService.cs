using LearnEase.Core.Models;
using Microsoft.AspNetCore.Http;

namespace LearnEase.Core.Services;

public interface ICourseService
{
    Task<Course> GetCourseByIdAsync(int courseId);

    Task<IEnumerable<Course>> GetAllCoursesAsync();

    Task DeleteCourseByIdAsync(int courseId);
    
    Task CreateCourseAsync(Course newCourse);

    Task SetCourseLogo(Course course, IFormFile? logo);

    Task DeleteCourseLogo(int courseId);
}