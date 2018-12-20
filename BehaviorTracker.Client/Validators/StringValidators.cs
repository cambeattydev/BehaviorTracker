using System.Collections.Generic;
using FluentValidation;

namespace BehaviorTracker.Client.Validators
{
    public static class StringValidators
    {
        public static IRuleBuilderOptions<T, string> NotNullOrWhitespace<T>(this IRuleBuilder<T,string> ruleBuilder) {
            return ruleBuilder.Must(t => !string.IsNullOrWhiteSpace(t)).WithMessage("{PropertyName} must not be empty");
        }
    }
}