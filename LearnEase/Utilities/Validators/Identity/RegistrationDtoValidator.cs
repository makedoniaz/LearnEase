using FluentValidation;
using LearnEase.Dtos;

namespace LearnEase.Utilities.Validators.Identity
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator()
        {
            base.RuleFor(r => r.Name)
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
}