using Moq;
using LearnEase.Repositories.Interfaces;
using LearnEase.Models;
using LearnEase.Services;

namespace UnitTests.Services
{
    public class CourseServiceTests
    {
        [Fact]
        public async Task GetCourseByIdAsync_CourseFound_DoesntThrowException()
        {
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();

            courseRepositoryMock.Setup((repo) => repo.GetByIdAsync(0))
                .ReturnsAsync(new Course() {
                    Name = "test"
                });

            var courseService = new CourseService(courseRepository: courseRepositoryMock.Object);

            await courseService.GetCourseByIdAsync(0);
        }

        [Fact]
        public async Task GetCourseByIdAsync_CourseNotFound_ThrowsArgumentException()
        {
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();

            courseRepositoryMock.Setup((repo) => repo.GetByIdAsync(0))
                .ReturnsAsync(value: null);

            var courseService = new CourseService(courseRepository: courseRepositoryMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await courseService.GetCourseByIdAsync(0));
        }


        [Fact]
        public async Task CreateCourseAsync_CourseCreated_DoesntThrowException()
        {
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();
            var course = new Course() 
                {
                    Name = "test", 
                    Description = "test", 
                    AmountOfLectures = 1, 
                    CreationDate = DateTime.Now
                };


            courseRepositoryMock.Setup((repo) => repo.CreateAsync(course))
                .ReturnsAsync(value: 1);

            var courseService = new CourseService(courseRepository: courseRepositoryMock.Object);
            await courseService.CreateCourseAsync(course);
        }


        [Fact]
        public async Task CreateCourseAsync_CourseEssentialDataIsNull_ThrowsException()
        {
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();
            var course = new Course() 
                {
                    Name = null, 
                    Description = null, 
                    AmountOfLectures = 1, 
                    CreationDate = DateTime.Now
                };


            courseRepositoryMock.Setup((repo) => repo.CreateAsync(course))
                .ReturnsAsync(0);

            var courseService = new CourseService(courseRepository: courseRepositoryMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await courseService.CreateCourseAsync(course));
        }

        [Fact]
        public async Task DeleteCourseByIdAsync_CourseDeleted_DoesntThrowException()
        {
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();


            courseRepositoryMock.Setup((repo) => repo.DeleteAsync(0))
                .ReturnsAsync(1);

            var courseService = new CourseService(courseRepository: courseRepositoryMock.Object);
            await courseService.DeleteCourseByIdAsync(0);
        }


        [Fact]
        public async Task DeleteCourseByIdAsync_CourseNotFound_ThrowsException()
        {
            Mock<ICourseRepository> courseRepositoryMock = new Mock<ICourseRepository>();


            courseRepositoryMock.Setup((repo) => repo.DeleteAsync(0))
                .ReturnsAsync(0);

            var courseService = new CourseService(courseRepository: courseRepositoryMock.Object);
            await Assert.ThrowsAsync<Exception>(async () => await courseService.DeleteCourseByIdAsync(0));
        }
    }
}