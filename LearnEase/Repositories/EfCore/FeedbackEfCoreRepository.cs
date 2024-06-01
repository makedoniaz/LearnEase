using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Data;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repositories.EfCore
{
    public class FeedbackEfCoreRepository : IFeedbackRepository
    {
        private readonly LearnEaseDbContext _context;

        public FeedbackEfCoreRepository(LearnEaseDbContext context) => _context = context;

        public async Task<IEnumerable<Feedback>> GetAllByCourseIdAsync(int courseId)
        {
            return await _context.Feedbacks
                .Where(f => f.CourseId == courseId)
                .OrderByDescending(f => f.CreationDate)
                .ToListAsync();
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _context.Feedbacks.FirstOrDefaultAsync((f) => f.Id == id);
        }

        public async Task CreateAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int feedbackId)
        {
            await _context.Feedbacks.Where((f) => f.Id == feedbackId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<int> PutAsync(int id, Feedback feedback)
        {
            return await _context.Feedbacks.Where((f) => f.Id == id)
                .ExecuteUpdateAsync((setters) => setters
                    .SetProperty(f => f.Text, feedback.Text)
                    .SetProperty(f => f.Rating, feedback.Rating)
                    .SetProperty(f => f.CreationDate, DateTime.Now)
                );
        }
    }
}