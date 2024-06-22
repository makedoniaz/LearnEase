using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LearnEase.Models;

namespace LearnEase.Utilities.Validators
{
    public class FeedbackValidator : AbstractValidator<Feedback>
    {
        public FeedbackValidator()
        {
            base.RuleFor(f => f.Username)
                .NotEmpty()
                .MaximumLength(100);

             base.RuleFor(f => f.Text)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}