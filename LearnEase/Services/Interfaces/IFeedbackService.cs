using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface IFeedbackService
    {
        int CurrentCourseId { get; set; }

        Task<Feedback> GetFeedbackById(int id);

        Task<IEnumerable<Feedback>> GetAllFeedbacksByCourseIdAsync(int courseId);
        
        Task CreateFeedbackAsync(Feedback newFeedback);

        Task PutFeedbackAsync(int id, Feedback feedback);

        Task DeleteFeedbackAsync(int id);
    }
}