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

        private bool _wasValidated;

        protected IDictionary<string, IEnumerable<string>> Errors;


        [Parameter] protected T Model { get; set; }

        protected override void OnInit()
        {
            _validator = _validatorFactory.GetValidator<T>();
            Errors = new Dictionary<string, IEnumerable<string>>();
        }

        protected bool IsValid(string propertyName) =>
            Errors.ContainsKey(propertyName) && !Errors[propertyName].Any();

        protected bool IsInvalid(string propertyName) =>
            Errors.ContainsKey(propertyName) && Errors[propertyName].Any();

        protected string ValidationClassName(string propertyName) =>
            IsValid(propertyName) ? "is-valid" : IsInvalid(propertyName) ? "is-invalid" : "";

        //protected string ValidationHasHappened => _wasValidated ? "was-validated" : "";
        
        
        protected async Task Validate(string propertyName)
        {
            var context = new ValidationContext<T>(Model, new PropertyChain(),
                new MemberNameValidatorSelector(new[] {propertyName}));
            var validationResult = await _validator.ValidateAsync(context);
            if (!validationResult.IsValid)
                Errors[propertyName] = validationResult.Errors.Where(error => error.PropertyName == propertyName)
                    .Select(s => s.ErrorMessage);
            else
                Errors[propertyName] = new string[0];
            
            _wasValidated = true;
        }

        protected async Task Validate()
        {
            var validationResult = await _validator.ValidateAsync(Model);
            var propertyNames = Model.GetType().GetProperties().Select(s => s.Name);
            Console.WriteLine("------------PropertyNames-------------");
            Console.WriteLine(string.Join("\n", propertyNames));
            Console.WriteLine("-----------End PropertyNames-----------");
            foreach (var propertyName in propertyNames)
            {
                var propertyErrors = validationResult.Errors.Where(error => error.PropertyName == propertyName);
                if (propertyErrors.Any())
                {
                    Errors[propertyName] = propertyErrors.Select(s => s.ErrorMessage);
                }
                else
                {
                    Errors[propertyName] = new string[0];
                }
            }

            _wasValidated = true;
        }
    }
}