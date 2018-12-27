using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BehaviorTracker.Client.Models;
using BehaviorTracker.Client.Validators;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Shared.Admin
{
    public class AdminStudentModel : ValidationComponent<Student>
    {
        [Inject] protected HttpClient _httpClient { get; set; }

        Client.Models.Student OriginalModel { get; set; }

        [Parameter] Action<Models.Student> DeleteStudent { get; set; }

        //[CascadingParameter] Modal _modal { get; set; }

        protected bool _editMode;

        protected override void OnInit()
        {
            Console.WriteLine("Before call to base.OnInit");
            base.OnInit();
            Console.WriteLine("After call to base.OnInit");
            
            if (Model == null)
            {
                Model = new Models.Student();
            }

            if (Model.StudentKey < 1)
            {
                _editMode = true;
            }
            
            
        }

        protected void Delete()
        {
            DeleteStudent(Model);
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
                DeleteStudent(Model);
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
                        StudentKey = Model.StudentKey,
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
                StudentKey = Model.StudentKey,
                AvailableAnswers = new List<Models.GoalAvailableAnswer>(),
                GoalKey = minGoalKey > 0 ? 0 : minGoalKey
            };

            var goals = Model.Goals.ToList();
            goals.Add(newGoal);
            Model.Goals = goals;
            base.StateHasChanged();
        }

        protected void DeleteGoal(Models.Goal goal)
        {
            var goals = Model.Goals.ToList();
            var deleted = goals.Remove(goal);
            if (deleted)
            {
                Model.Goals = goals;
                base.StateHasChanged();
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
                    Model = newModel;
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