using FluentValidation;
using LearnEase.Models;

namespace LearnEase.Utilities.Validators;

public class UserValidator : AbstractValidator<User>
{
     public UserValidator()
        {
            base.RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(100);

            base.RuleFor(u => u.Email)
                .NotEmpty()
                .MaximumLength(100);

            base.RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(7)
                .MaximumLength(50);
        }
}
