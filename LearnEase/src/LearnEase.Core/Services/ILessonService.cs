using LearnEase.Core.Models;

namespace LearnEase.Core.Services;

public interface ILessonService
{
    Task<Lesson> GetLessonByIdAsync(int lessonId);

    Task DeleteLessonByIdAsync(int lessonId);
    
    Task CreateLessonAsync(Lesson newLesson);

    Task PutLessonAsync(int id, Lesson lesson);
}
