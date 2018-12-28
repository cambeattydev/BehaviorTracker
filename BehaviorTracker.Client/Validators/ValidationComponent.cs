using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Validators
{
    public class ValidationComponent<T> : BlazorComponent
    {
        [Inject] protected IValidatorFactory _validatorFactory { get; set; }

        private IValidator<T> _validator;

        protected IDictionary<string, IEnumerable<string>> Errors;


        [Parameter] protected T Model { get; set; }

        protected override void OnInit()
        {
            Console.WriteLine("Begin on init of ValidationComponent");
            _validator = _validatorFactory.GetValidator<T>();
            Errors = new Dictionary<string, IEnumerable<string>>();
            Console.WriteLine("End on init of ValidationComponent");
        }

        protected bool IsValid(string propertyName) =>
            Errors.ContainsKey(propertyName) && !Errors[propertyName].Any();

        protected bool IsInvalid(string propertyName) =>
            Errors.ContainsKey(propertyName) && Errors[propertyName].Any();

        protected string ValidationClassName(string propertyName) =>
            IsValid(propertyName) ? "is-valid" : IsInvalid(propertyName) ? "is-invalid" : string.Empty;

        protected IEnumerable<string> ErrorsOf(string propertyName) =>
            Errors.ContainsKey(propertyName) ? Errors[propertyName] : new string[0];

        //protected string ValidationHasHappened => _wasValidated ? "was-validated" : "";
        
        
        protected async Task<ValidationResult> ValidateAsync(string propertyName)
        {
            var context = new ValidationContext<T>(Model, new PropertyChain(),
                new MemberNameValidatorSelector(new[] {propertyName}));
            var validationResult = await _validator.ValidateAsync(context);
            if (!validationResult.IsValid)
                Errors[propertyName] = validationResult.Errors.Where(error => error.PropertyName == propertyName)
                    .Select(s => s.ErrorMessage);
            else
                Errors[propertyName] = new string[0];

            return validationResult;
        }

        protected async Task<ValidationResult> ValidateAsync()
        {
            var validationResult = await _validator.ValidateAsync(Model);
            Console.WriteLine($"ValidationResult.IsValid:{validationResult.IsValid}");
            var propertyNames = Model.GetType().GetProperties().Select(s => s.Name);
            Console.WriteLine("------------PropertyNames-------------");
            Console.WriteLine(string.Join("\n", propertyNames));
            Console.WriteLine("-----------End PropertyNames-----------");
            foreach (var propertyName in propertyNames)
            {
                var propertyErrors = validationResult.Errors.Where(error => error.PropertyName == propertyName);
                Console.WriteLine($"Errors for [{propertyName}]: {propertyErrors.Count()}");
                if (propertyErrors.Any())
                {
                    Errors[propertyName] = propertyErrors.Select(s => s.ErrorMessage);
                }
                else
                {
                    Errors[propertyName] = new string[0];
                }
            }
            Console.WriteLine($"After setting Errors ValidationResult.IsValid:{validationResult.IsValid}");
            return validationResult;
        }
    }
}