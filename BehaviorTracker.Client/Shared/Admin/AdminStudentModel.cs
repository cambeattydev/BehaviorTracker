using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BehaviorTracker.Client.Models;
using BehaviorTracker.Client.Validators;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;

namespace BehaviorTracker.Client.Shared.Admin
{
    public class AdminStudentModel : ValidationComponent<Student>
    {
        //[CascadingParameter] Modal _modal { get; set; }

        protected bool _editMode;
        [Inject] private HttpClient _httpClient { get; set; }

        Client.Models.Student OriginalModel { get; set; }

        [Parameter] Func<Models.Student, Task> DeleteStudentAsync { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            Console.WriteLine("AdminStudentModel: Begin of OnInit");
            if (Model == null)
            {
                Model = new Models.Student();
            }

            if (Model.StudentKey < 1)
            {
                _editMode = true;
            }

            Console.WriteLine("AdminStudentModel: End of OnInit");
        }

        protected async Task Delete()
        {
            await DeleteStudentAsync(Model);
        }

        protected void Edit()
        {
            _editMode = true;
            OriginalModel = Model.Copy();
        }

        protected void Cancel()
        {
            if (Model.StudentKey > 0)
            {
                Model = OriginalModel;
                _editMode = false;
            }
            else
            {
                DeleteStudentAsync(Model);
            }
        }

        protected void AddGoal()
        {
            if (Model.Goals == null || !Model.Goals.Any())
            {
                Model.Goals = new List<Models.Goal>
                {
                    new Models.Goal()
                    {
                        BehaviorTrackerUserKey = Model.StudentKey,
                        AvailableAnswers = new List<Models.GoalAvailableAnswer>(),
                        GoalKey = 0
                    }
                };
                base.StateHasChanged();
                return;
            }

            var minGoalKey = Model.Goals.Min(s => s.GoalKey);
            var newGoal = new Models.Goal()
            {
                BehaviorTrackerUserKey = Model.StudentKey,
                AvailableAnswers = new List<Models.GoalAvailableAnswer>(),
                GoalKey = minGoalKey > 0 ? 0 : minGoalKey
            };

            Model.Goals.Add(newGoal);
            base.StateHasChanged();
        }

        protected async Task DeleteGoalAsync(Models.Goal goal)
        {
            if (goal.GoalKey < 1)
            {
                var deleted = Model.Goals.Remove(goal);
                if (deleted)
                    base.StateHasChanged();
                return;
            }

            var deletedGoalMessage = await _httpClient.DeleteAsync($"api/Goal/Delete/{goal.GoalKey}");
            if (deletedGoalMessage.IsSuccessStatusCode)
            {
                var stringDeletedGoal = await deletedGoalMessage.Content.ReadAsStringAsync();
                var deletedGoal = Json.Deserialize<Models.Goal>(stringDeletedGoal);
                var deleted = Model.Goals.RemoveAll(listGoal => listGoal.GoalKey == deletedGoal.GoalKey);
                Console.WriteLine($"Deleted:{deleted}");
                if (deleted > 0)
                {
                    base.StateHasChanged();
                }
            }
        }

        //    async Task ShowModal()
        //    {
        //        _modal.ElementId = $"DeleteStudent{Model.StudentKey}";
        //        _modal.BodyHtml = builder =>
        //        {
        //            builder.OpenElement(0, "p");
        //            builder.AddContent(1, $"Are you sure you want to delete {Model.StudentFirstName} {Model.StudentLastName}");
        //            builder.CloseElement();
        //        };
        //        _modal.IsDelete = true;
        //        _modal.ConfirmText = "Delete";
        //        _modal.OnSubmit = Delete;
        //        _modal.StateHasChanged();
        //        
        //        await JSRuntime.Current.InvokeAsync<string>("modal.show",_modal.ElementId );        
        //    }

        protected async Task Save()
        {
            var validationResult = await ValidateAsync();
            Console.WriteLine($"On Component ValidationResult.IsValid:{validationResult.IsValid}");
            if (validationResult.IsValid)
            {
                try
                {
                    var newModel =
                        await _httpClient.PostJsonAsync<Client.Models.Student>("/api/Student/Student", Model);
                    Model.StudentKey = newModel.StudentKey;
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