using LearnEase.Core.Models;
using LearnEase.Core.Repositories.Base;

namespace LearnEase.Core.Repositories;

public interface ICourseRepository : IGetByIdAsync<Course, int>, IGetAllAsync<Course>, 
ICreateAsync<Course>, IDeleteAsync<int>
{
    
}
