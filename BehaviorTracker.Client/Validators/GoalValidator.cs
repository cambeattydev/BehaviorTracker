using BehaviorTracker.Client.Models;
using BehaviorTracker.Shared;
using FluentValidation;

namespace BehaviorTracker.Client.Validators
{
    public class GoalValidator : AbstractValidator<Goal>
    {
        public GoalValidator()
        {
            RuleFor(m => m.GoalDescription).NotNullOrWhitespace();
            RuleFor(m => m.GoalType).NotEqual(GoalType.None).WithMessage("{PropertyName} can not be empty");
            RuleFor(m => m.BehaviorTrackerUserKey).GreaterThanOrEqualTo(1)
                .WithMessage("A goal must be attached to a student");
        }
    }
}