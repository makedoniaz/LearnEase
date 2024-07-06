namespace LearnEase.Core.Models;

public class Course
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public int AmountOfLectures { get; set; }

    public DateTime CreationDate { get; set; }

    public string? CourseLogoPath { get; set; }
}
