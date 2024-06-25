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

        public async Task<Course> GetCourseById(int courseId)
        {
            var course = await courseRepository.GetByIdAsync(courseId);

            if (course is null)
                throw new ArgumentException($"Cannot find course by id: {courseId}.");

            return course;
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

            var changesCount = await this.courseRepository.CreateAsync(newCourse);

            if (changesCount == 0)
                throw new Exception("Course creation didn't apply!");
        }

        public async Task SetCourseLogo(Course course, IFormFile? logo) {
            if (logo is null)
                return;

            var extension = new FileInfo(logo.FileName).Extension[1..];
            course.CourseLogoPath = $"Assets/Logos/{course.Id}.{extension}";

            using var newFileStream = File.Create(course.CourseLogoPath);
            await logo.CopyToAsync(newFileStream);
        }

        public async Task DeleteCourseByIdAsync(int courseId)
        {
            var changesCount = await courseRepository.DeleteAsync(courseId);

            if (changesCount == 0)
                throw new Exception("Course delete didn't apply!");
        }

        public async Task DeleteCourseLogo(int courseId)
        {
            var course = await courseRepository.GetByIdAsync(courseId);

            if (course is null || course.CourseLogoPath is null)
                return;

            File.Delete(course.CourseLogoPath);
        }
    }
}