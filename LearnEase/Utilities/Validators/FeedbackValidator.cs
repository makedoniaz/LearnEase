using FluentValidation;
using LearnEase.Models;

namespace LearnEase.Utilities.Validators
{
    public class FeedbackValidator : AbstractValidator<Feedback>
    {
        public FeedbackValidator()
        {
             base.RuleFor(f => f.Text)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}