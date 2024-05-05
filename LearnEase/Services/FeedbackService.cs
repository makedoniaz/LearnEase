using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;

namespace LearnEase.Services
{
    public class FeedbackService : IFeedbackService
    {
        public int CurrentCourseId { get; set; }

        private readonly IFeedbackRepository feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository) {
            this.feedbackRepository = feedbackRepository;
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            return await feedbackRepository.GetById(id);
        }

        public async Task PutFeedbackAsync(int id, Feedback feedback)
        {
            bool isNullInput = feedback.GetType().GetProperties()
                        .All(p => p.GetValue(feedback) != null);

            if (isNullInput)
                throw new ArgumentNullException(nameof(feedback));

            feedback.CreationDate = DateTime.Now;

            await feedbackRepository.PutAsync(id, feedback);
        }

        public async Task CreateFeedbackAsync(Feedback feedback)
        {
            feedback.CreationDate = DateTime.Now;
            feedback.CourseId = this.CurrentCourseId;

            await feedbackRepository.CreateAsync(feedback);
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            await feedbackRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksByCourseIdAsync(int courseId)
        {
            CurrentCourseId = courseId;
            return await feedbackRepository.GetAllByCourseIdAsync(courseId);
        }
    }
}