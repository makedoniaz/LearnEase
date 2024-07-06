using LearnEase.Core.Models;

namespace LearnEase.Core.Services;

public interface IFeedbackService
{
    Task<Feedback> GetFeedbackById(int id);

    Task<IEnumerable<Feedback>> GetAllFeedbacksByCourseIdAsync(int courseId);
    
    Task CreateFeedbackAsync(Feedback newFeedback, int courseId);

    Task PutFeedbackAsync(int id, Feedback feedback);

    Task DeleteFeedbackAsync(int id);
}
