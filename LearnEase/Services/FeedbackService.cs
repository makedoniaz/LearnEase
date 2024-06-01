using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;

namespace LearnEase.Services
{
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
            var hasChanged = await feedbackRepository.PutAsync(id, feedback) == 1;

            if (!hasChanged)
                throw new ArgumentException($"Cannot change feedback.");
        }

        public async Task CreateFeedbackAsync(Feedback feedback, int courseId)
        {
            feedback.CreationDate = DateTime.Now;
            feedback.CourseId = courseId;

            await feedbackRepository.CreateAsync(feedback);
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            await feedbackRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksByCourseIdAsync(int courseId)
        {
            return await feedbackRepository.GetAllByCourseIdAsync(courseId);
        }
    }
}