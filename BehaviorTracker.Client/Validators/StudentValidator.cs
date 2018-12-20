using BehaviorTracker.Client.Models;
using FluentValidation;

namespace BehaviorTracker.Client.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(m => m.StudentFirstName).NotNullOrWhitespace();
            RuleFor(m => m.StudentLastName).NotNullOrWhitespace();
        }
    }
}