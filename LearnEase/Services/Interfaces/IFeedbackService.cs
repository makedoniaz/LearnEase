using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacksByCourseIdAsync(int courseId);
        
        Task CreateFeedbackAsync(Feedback newFeedback);

        Task PutFeedbackAsync(int id, Feedback feedback);

        Task DeleteFeedbackAsync(Feedback feedback);
    }
}