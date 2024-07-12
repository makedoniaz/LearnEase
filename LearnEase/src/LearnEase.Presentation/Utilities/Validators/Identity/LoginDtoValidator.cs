using FluentValidation;
using LearnEase.Core.Dtos;

namespace LearnEase.Presentation.Utilities.Validators.Identity;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        base.RuleFor(l => l.Login)
            .NotEmpty()
            .MaximumLength(100);

        base.RuleFor(l => l.Password)
            .NotEmpty()
            .MaximumLength(50);
    }
}
