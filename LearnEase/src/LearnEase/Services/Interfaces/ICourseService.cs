using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Course> GetCourseByIdAsync(int courseId);

        Task<IEnumerable<Course>> GetAllCoursesAsync();

        Task DeleteCourseByIdAsync(int courseId);
        
        Task CreateCourseAsync(Course newCourse);

        Task SetCourseLogo(Course course, IFormFile? logo);

        Task DeleteCourseLogo(int courseId);
    }
}