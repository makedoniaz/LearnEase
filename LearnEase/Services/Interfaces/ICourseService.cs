using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        
        Task CreateCourseAsync(Course newCourse);

        Task SetCourseLogo(Course course, IFormFile logo);
    }
}