using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task PutFeedbackAsync(int id, Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            await feedbackRepository.PutAsync(id, feedback);
        }

        public async Task CreateFeedbackAsync(Feedback feedback)
        {
            await feedbackRepository.CreateAsync(feedback);
        }

        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            await feedbackRepository.DeleteAsync(feedbackId);
        }

        public Task<IEnumerable<Feedback>> GetAllFeedbacksByCourseIdAsync(int courseId)
        {
            return feedbackRepository.GetAllByCourseIdAsync(courseId);
        }
    }
}