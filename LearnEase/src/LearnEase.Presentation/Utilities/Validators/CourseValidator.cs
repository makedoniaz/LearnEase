using FluentValidation;
using LearnEase.Core.Models;

namespace LearnEase.Presentation.Utilities.Validators;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        base.RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        base.RuleFor(c => c.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}
