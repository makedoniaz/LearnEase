using FluentValidation;
using LearnEase.Core.Models;

namespace LearnEase.Presentation.Utilities.Validators;

public class FeedbackValidator : AbstractValidator<Feedback>
{
    public FeedbackValidator()
    {
            base.RuleFor(f => f.Text)
            .NotEmpty()
            .MaximumLength(500);
    }
}
