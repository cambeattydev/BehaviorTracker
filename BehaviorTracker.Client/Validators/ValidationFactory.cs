using System;
using System.Collections.Generic;
using BehaviorTracker.Client.Models;
using FluentValidation;

namespace BehaviorTracker.Client.Validators
{
    public class ValidationFactory : IValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ValidationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private readonly Dictionary<Type, IValidator> _validators = new Dictionary<Type, IValidator>()
        {
            {typeof(Student), new StudentValidator()}
        };

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>) _serviceProvider.GetService(typeof(IValidator<T>));
        }

        public IValidator GetValidator(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return _serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(type)) as IValidator;
        }
    }
}