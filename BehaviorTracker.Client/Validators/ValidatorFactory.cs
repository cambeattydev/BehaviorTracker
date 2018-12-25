using System;
using FluentValidation;

namespace BehaviorTracker.Client.Validators
{
    public class ValidatorFactory : IValidatorFactory
    {
        public IValidator<T> GetValidator<T>()
        {
            throw new NotImplementedException();
        }

        public IValidator GetValidator(Type type)
        {
            throw new NotImplementedException();
        }
    }
}