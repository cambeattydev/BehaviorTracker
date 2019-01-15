using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BehaviorTracker.Client.Models;
using BehaviorTracker.Client.Validators;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Shared.Admin
{
    public class AdminNumericOptionValueModel : ValidationComponent<GoalAvailableAnswerEditModel>
    {
        [Inject] protected HttpClient _httpClient { get; set; }
        private Goal _goal;
        [Parameter] protected Goal Goal
        {
            get => _goal;
            set
            {
                _goal = value;
                SetPropertiesBasedOnGoal();
            }
        }

        GoalAvailableAnswerEditModel OriginalModel;

        protected bool _editMode;

        protected bool canDelete;

        private void SetPropertiesBasedOnGoal()
        {
            if (Goal != null)
            {
                Model = new GoalAvailableAnswerEditModel(Goal);
                canDelete = Goal.AvailableAnswers?.Any() ?? false;
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
            if (Model == null)
            {
                Model = new GoalAvailableAnswerEditModel();
            }
            SetPropertiesBasedOnGoal();
        }

        protected void Edit()
        {
            OriginalModel = Model.Copy();
            _editMode = true;
        }

        protected async Task Delete()
        {
            var deleteResponse =
                await _httpClient.DeleteAsync($"api/GoalAvailableAnswer/DeleteAllForGoal/{Goal.GoalKey}");
            if (deleteResponse.IsSuccessStatusCode)
            {
                Goal.AvailableAnswers = new List<GoalAvailableAnswer>();
                Model = new GoalAvailableAnswerEditModel
                {
                    GoalKey = Goal.GoalKey
                };
            }
        }

        protected void Cancel()
        {
            Model = OriginalModel;
            _editMode = false;
        }

        protected async Task Save()
        {
            var validationResult = await ValidateAsync().ConfigureAwait(false);
            if (validationResult.IsValid)
            {
                var savedOptions = new List<GoalAvailableAnswer>();
                for (var i = Model.StartValue; i <= Model.EndValue; i = i + Model.StepAmount)
                {
                    savedOptions.Add(new GoalAvailableAnswer
                    {
                        Goal = Goal,
                        GoalKey = Goal.GoalKey,
                        OptionValue = i.ToString(CultureInfo.InvariantCulture)
                    });
                }

                var savedGoalAvailableAnswers = await 
                    _httpClient.PostJsonAsync<IList<GoalAvailableAnswer>>("api/GoalAvailableAnswer/DeleteAndInsert",
                        savedOptions).ConfigureAwait(false);
                _editMode = false;
                Goal.AvailableAnswers = savedGoalAvailableAnswers;
            }
          
        }
    }
}