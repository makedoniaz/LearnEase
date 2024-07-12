#pragma warning disable CS8618

namespace LearnEase.Core.Models;

public class Feedback
{
    public int Id { get; set; }

    public required string Text { get; set; }

    public int? Rating { get; set; }

    public int? CourseId { get; set; }

    public Course Course { get; set; }

    public DateTime CreationDate { get; set; }

    public string? UserId { get; set; }

    public User User { get; set; }

    public string? Username { get; set; }
}