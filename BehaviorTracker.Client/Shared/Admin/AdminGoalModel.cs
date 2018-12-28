using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BehaviorTracker.Client.Models;
using BehaviorTracker.Client.Validators;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Shared.Admin
{
    public class AdminGoalModel : ValidationComponent<Goal>
    {

        [Inject] private HttpClient _httpClient { get; set; }

        private Client.Models.Goal OriginalModel { get; set; }

        [Parameter] protected Func<Models.Goal, Task> DeleteGoalAsync { get; set; }

        //[CascadingParameter] Modal _modal { get; set; }

        protected bool _editMode;

        protected override void OnInit()
        {
            base.OnInit();
            Console.WriteLine("Begin of OnInit");
            if (Model == null)
            {
                Model = new Goal();
            }

            if (Model.GoalKey < 1)
            {
                _editMode = true;
            }
            Console.WriteLine("End of OnInit");
        }

        protected async Task Delete()
        {
            await DeleteGoalAsync(Model);
        }

        protected void Edit()
        {
            _editMode = true;
            OriginalModel = Model.Copy();
        }

        protected async Task Cancel()
        {
            if (Model.GoalKey > 0)
            {
                Model = OriginalModel;
                _editMode = false;
            }
            else
            {
                await DeleteGoalAsync(Model);
            }
        }

        protected async Task Save()
        {
            var validationResult = await ValidateAsync();
            if (validationResult.IsValid)
            {
                try
                {
                    var newModel = await _httpClient.PostJsonAsync<Client.Models.Goal>("/api/Goal/Goal", Model);
                    Model.GoalKey = newModel.GoalKey;
                    base.StateHasChanged();
                    _editMode = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}