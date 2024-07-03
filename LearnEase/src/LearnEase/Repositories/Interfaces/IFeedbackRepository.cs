using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces
{
    public interface IFeedbackRepository :
        IGetByIdAsync<Feedback, int>, ICreateAsync<Feedback>, 
        IDeleteAsync<int>, IPutAsync<int, Feedback>
    {
        Task<IEnumerable<Feedback>> GetAllByCourseIdAsync(int courseId);
    }
}