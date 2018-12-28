using BehaviorTracker.Client.Models;
using FluentValidation;

namespace BehaviorTracker.Client.Validators
{
    public class GoalAvailableAnswerEditModelValidator : AbstractValidator<GoalAvailableAnswerEditModel>
    {
        public GoalAvailableAnswerEditModelValidator()
        {
            RuleFor(m => m.GoalKey).GreaterThan(0);
            //RuleFor(m => m.)
        }
    }
}