using LearnEase.Core.Data;
using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Infrastructure.Repositories.EfCore;

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

    public async Task<int> CreateAsync(Feedback feedback)
    {
        _context.Feedbacks.Add(feedback);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<int> DeleteAsync(int feedbackId)
    {
        var feedbackToDelete = _context.Feedbacks.FirstOrDefault(f => f.Id == feedbackId);

        if (feedbackToDelete is null)
            return 0;

        _context.Feedbacks.Remove(feedbackToDelete);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<int> PutAsync(int id, Feedback feedback)
    {
        var feedbackToUpdate = _context.Feedbacks.FirstOrDefault(f => f.Id == id);

        if (feedbackToUpdate is null)
            return 0;
        
        feedbackToUpdate.Text = feedback.Text;
        feedbackToUpdate.Rating = feedback.Rating;
        feedbackToUpdate.CreationDate = DateTime.Now;

        _context.Feedbacks.Update(feedbackToUpdate);     
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }
}
