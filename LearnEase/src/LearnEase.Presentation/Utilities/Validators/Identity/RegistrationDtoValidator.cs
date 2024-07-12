using FluentValidation;
using LearnEase.Core.Dtos;

namespace LearnEase.Presentation.Utilities.Validators.Identity;

public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
{
    public RegistrationDtoValidator()
    {
        base.RuleFor(r => r.Login)
            .NotEmpty()
            .MaximumLength(100);

        base.RuleFor(r => r.Email)
            .NotEmpty()
            .MaximumLength(100);

        base.RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(7)
            .MaximumLength(50);
    }
}
