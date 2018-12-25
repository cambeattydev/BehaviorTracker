using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Validators
{
    public class ValidationComponent<T> : BlazorComponent
    {
        [Inject] protected IValidatorFactory _validatorFactory { get; set; }

        private IValidator<T> _validator;

        protected IDictionary<string, IEnumerable<string>> Errors = new Dictionary<string, IEnumerable<string>>();

        [Parameter] protected T Model { get; set; }

        protected override void OnInit()
        {
            _validator = _validatorFactory.GetValidator<T>();
        }

        protected async Task Validate(string propertyName)
        {
            var context = new ValidationContext<T>(Model, new PropertyChain(),
                new MemberNameValidatorSelector(new[] {propertyName}));
            var validationResult = await _validator.ValidateAsync(context);
            if (!validationResult.IsValid)
                Errors[propertyName] = validationResult.Errors.Where(error => error.PropertyName == propertyName)
                    .Select(s => s.ErrorMessage);
        }

        protected async Task Validate()
        {
            var validationResult = await _validator.ValidateAsync(Model);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (Errors.ContainsKey(error.PropertyName))
                    {
                        Errors[error.PropertyName] = Errors[error.PropertyName].Append(error.ErrorMessage);
                    }
                    else
                    {
                        Errors[error.PropertyName] = new[] {error.ErrorMessage};
                    }
                }
            }
        }
    }
}