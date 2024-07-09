using LearnEase.Core.Models;
using LearnEase.Core.Repositories.Base;

namespace LearnEase.Core.Repositories;

public interface ILessonRepository : IGetByIdAsync<Lesson, int>, ICreateAsync<Lesson>, 
    IDeleteAsync<int>, IPutAsync<int, Lesson>
{

}
