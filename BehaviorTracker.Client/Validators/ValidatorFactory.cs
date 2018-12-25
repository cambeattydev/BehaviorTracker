using System;
using FluentValidation;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Validators
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IValidator<T> GetValidator<T>()
        {
           return (IValidator<T>) _serviceProvider.GetService(typeof(IValidator<T>));
        }

        public IValidator GetValidator(Type type)
        {
            return _serviceProvider.GetService(type) as IValidator;
        }
    }
}