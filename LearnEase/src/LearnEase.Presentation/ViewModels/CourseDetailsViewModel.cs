using LearnEase.Core.Models;

namespace LearnEase.Presentation.ViewModels;

public class CourseDetailsViewModel
{
    public required Course Course { get; set; }
    
    public required IEnumerable<Lesson> Lessons { get; set; }

    public required IEnumerable<Feedback> Feedbacks { get; set; }
}
