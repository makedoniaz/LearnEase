namespace LearnEase.Core.Models;

public class Feedback
{
    public int Id { get; set; }

    public required string Text { get; set; }

    public int? Rating { get; set; }

    public int CourseId { get; set; }

    public DateTime CreationDate { get; set; }

    public string? Username { get; set; }
}