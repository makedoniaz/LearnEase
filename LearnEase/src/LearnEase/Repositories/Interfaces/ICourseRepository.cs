using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces
{
    public interface ICourseRepository : IGetByIdAsync<Course, int>, IGetAllAsync<Course>, 
    ICreateAsync<Course>, IDeleteAsync<int>
    {
        
    }
}