using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using LearnEase.Core.Services;

namespace LearnEase.Infrastructure.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository feedbackRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository) {
        this.feedbackRepository = feedbackRepository;
    }

    public async Task<Feedback> GetFeedbackById(int id)
    {
        var feedback = await feedbackRepository.GetByIdAsync(id);

        if (feedback is null)
            throw new ArgumentException($"Cannot find feedback by id: {id}.");

        return feedback;
    }

    public async Task PutFeedbackAsync(int id, Feedback feedback)
    {
        var changesCount = await feedbackRepository.PutAsync(id, feedback);

        if (changesCount == 0)
            throw new Exception("Feedback change didn't apply!");
    }

    public async Task CreateFeedbackAsync(User user, Feedback feedback)
    {   
        if (feedback.UserId is not null)
            feedback.Username = user?.UserName;

        feedback.CreationDate = DateTime.Now;

        var changesCount = await feedbackRepository.CreateAsync(feedback);

        if (changesCount == 0)
            throw new Exception("Feedback creation didn't apply!");
    }

    public async Task DeleteFeedbackAsync(int id)
    {
        var changesCount = await feedbackRepository.DeleteAsync(id);

        if (changesCount == 0)
            throw new Exception("Feedback delete didn't apply!");
    }

    public async Task<IEnumerable<Feedback>> GetAllFeedbacksByCourseIdAsync(int courseId)
    {
        var feedbacks = await feedbackRepository.GetAllByCourseIdAsync(courseId);
        return feedbacks;
    }

    public async Task DeleteFeedbacksByCourseId(int courseId) {
        await feedbackRepository.DeleteByCourseId(courseId);
    }
}
