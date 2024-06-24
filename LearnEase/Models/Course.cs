namespace LearnEase.Models;

public class Course
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int AmountOfLectures { get; set; }

    public DateTime CreationDate { get; set; }

    public string? CourseLogoPath { get; set; }

    public User? User { get; set; }

    public int? UserId { get; set; }
}
