using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace LearnEase.Infrastructure.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository lessonRepository;

    private readonly UserManager<User> userManager;

    public LessonService(ILessonRepository lessonRepository, UserManager<User> userManager) {
        this.lessonRepository = lessonRepository;
        this.userManager = userManager;
    }

    public async Task<Lesson> GetLessonByIdAsync(int lessonId)
    {
        var lesson = await lessonRepository.GetByIdAsync(lessonId);

        if (lesson is null)
            throw new ArgumentException($"Cannot find feedback by id: {lessonId}.");

        return lesson;
    }

    public async Task CreateLessonAsync(User user, Lesson newLesson)
    {
        newLesson.AuthorName = user.UserName;
        newLesson.Timestamp = DateTime.Now;

        var changesCount = await lessonRepository.CreateAsync(newLesson);

        if (changesCount == 0)
            throw new Exception("Lesson creation didn't apply!");
    }

    public async Task DeleteLessonByIdAsync(int lessonId)
    {
        var changesCount = await lessonRepository.DeleteAsync(lessonId);

        if (changesCount == 0)
            throw new Exception("Lesson delete didn't apply!");
    }

    public async Task PutLessonAsync(int id, Lesson lesson)
    {
        var changesCount = await lessonRepository.PutAsync(id, lesson);

        if (changesCount == 0)
            throw new Exception("Lesson change didn't apply!");
    }
}
