using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LearnEase.Models;

namespace LearnEase.Utilities.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            base.RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(5);

            base.RuleFor(c => c.Description)
                .NotEmpty()
                .MaximumLength(500);

            base.RuleFor(c => c.AmountOfLectures)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}