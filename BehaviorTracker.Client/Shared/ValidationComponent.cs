using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Client.Validators;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Shared
{
    public class ValidationComponent<T> : BlazorComponent
    {
        private IValidator<T> _studentValidator;

        public ValidationComponent(IValidatorFactory validationFactory)
        {
            _studentValidator = validationFactory.GetValidator<T>();
        }

        protected IDictionary<string, IEnumerable<string>> Errors = new Dictionary<string, IEnumerable<string>>();

        [Parameter] protected T Model { get; set; }

        protected  async Task Validate(string propertyName)
        {
            var context = new ValidationContext<T>(Model, new PropertyChain(), new MemberNameValidatorSelector(new[] {propertyName}));
            var validationResult = await _studentValidator.ValidateAsync(context);
            if (validationResult.IsValid)
            {
                Errors[propertyName] = new string[0];
            }
            else
            {
                Errors[propertyName] = validationResult.Errors.Where(error => error.PropertyName == propertyName).Select(s => s.ErrorMessage);
            }
        }

        protected  async Task Validate()
        {
            var validationResult = await _studentValidator.ValidateAsync(Model);
            if (validationResult.IsValid)
            {
                Errors.Clear();
                return;
            }

            foreach (var errors in validationResult.Errors.GroupBy(g => g.PropertyName))
            {
                Errors[errors.Key] = errors.Select(s => s.ErrorMessage);
            }
        }
    }
}