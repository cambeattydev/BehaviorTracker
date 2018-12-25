using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Validators
{
    public class ValidationComponent<T> : BlazorComponent
    {
        [Inject] private IValidatorFactory _validatorFactory { get; set; }
        
        protected IDictionary<string, IEnumerable<string>> Errors = new Dictionary<string, IEnumerable<string>>();
        
        [Parameter] protected Client.Models.Student Model { get; set; }
    }
}