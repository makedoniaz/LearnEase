using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces
{
    public interface ICourseRepository : IGetAllAsync<Course>, ICreateAsync<Course>
    {
        
    }
}