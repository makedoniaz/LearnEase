using LearnEaseApp.Models;
using LearnEaseApp.ORMs;

namespace LearnEaseApp.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAll();

    Task<Course> GetById(int id);

    Task Create(CourseORM course);

    Task<int> DeleteById(int id);
}
