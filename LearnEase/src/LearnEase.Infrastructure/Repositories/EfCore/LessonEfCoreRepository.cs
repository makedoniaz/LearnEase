using LearnEase.Core.Data;
using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Infrastructure.Repositories.EfCore;

public class LessonEfCoreRepository : ILessonRepository
{
    private readonly LearnEaseDbContext _context;

    public LessonEfCoreRepository(LearnEaseDbContext context) => _context = context;

    public async Task<Lesson?> GetByIdAsync(int id)
    {
        return await _context.Lessons.FirstOrDefaultAsync((l) => l.Id == id);
    }

    public async Task<int> CreateAsync(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<int> DeleteAsync(int lessonId)
    {
        var lessonToDelete = _context.Feedbacks.FirstOrDefault(l => l.Id == lessonId);

        if (lessonToDelete is null)
            return 0;

        _context.Feedbacks.Remove(lessonToDelete);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<int> PutAsync(int id, Lesson lesson)
    {
        var lessonToUpdate = _context.Lessons.FirstOrDefault(l => l.Id == id);

        if (lessonToUpdate is null)
            return 0;
        
        
        lessonToUpdate.Name = lessonToUpdate.Name;
        lessonToUpdate.Description = lesson.Description;
        lessonToUpdate.VideoUrl = lesson.VideoUrl;
        lessonToUpdate.Timestamp = DateTime.Now;

        _context.Lessons.Update(lessonToUpdate);     
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }
}
