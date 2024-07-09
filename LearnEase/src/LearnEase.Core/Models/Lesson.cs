#pragma warning disable CS8618

namespace LearnEase.Core.Models;

public class Lesson
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? AuthorName { get; set; }
    
    public string? VideoUrl { get; set; }

    public string? Description { get; set; }

    public required Course Course { get; set; }

    public int CourseId { get; set; }

    public DateTime Timestamp { get; set; }

    public string? UserId { get; set; }

    public User User { get; set; }
}
