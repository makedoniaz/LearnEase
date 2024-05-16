using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;

namespace LearnEase.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await this.courseRepository.GetAllAsync();
        }

        public async Task CreateCourseAsync(Course newCourse)
        {
            if (string.IsNullOrWhiteSpace(newCourse.Name) || string.IsNullOrWhiteSpace(newCourse.Description))
                throw new ArgumentException();

            newCourse.CreationDate = DateTime.Now;

            await this.courseRepository.CreateAsync(newCourse);
        }
    }
}