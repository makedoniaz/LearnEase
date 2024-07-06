using LearnEase.Core.Models;
using LearnEase.Core.Repositories.Base;

namespace LearnEase.Core.Repositories;

public interface IFeedbackRepository :
    IGetByIdAsync<Feedback, int>, ICreateAsync<Feedback>, 
    IDeleteAsync<int>, IPutAsync<int, Feedback>
{
    Task<IEnumerable<Feedback>> GetAllByCourseIdAsync(int courseId);
}
