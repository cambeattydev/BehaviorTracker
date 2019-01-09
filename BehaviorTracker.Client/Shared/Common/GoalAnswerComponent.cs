using System;
using System.Net.Http;
using System.Threading.Tasks;
using BehaviorTracker.Client.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Shared.Common
{
    public class GoalAnswerComponent : BlazorComponent
    {
        [Parameter]
        protected string GoalName { get; set; } 
        [Parameter]
        protected GoalAnswer Answer { get; set; } 
        [Inject] HttpClient _httpClient { get; set; }
        protected async Task SaveAnswer()
        {
            if (Answer.GoalAnswerKey < 1)
            {
                var response = await _httpClient.PostJsonAsync<GoalAnswer>("/api/GoalAnswer/GoalAnswer", Answer);
                Answer.GoalAnswerKey = response.GoalAnswerKey;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Answer.Answer))
                {
                    Console.Write("Going to delete");
                    //Delete
                    var deleteResponseMessage = await _httpClient.DeleteAsync($"/api/GoalAnswer/GoalAnswer/{Answer.GoalAnswerKey}");
                    if (deleteResponseMessage.IsSuccessStatusCode)
                    {
                        var deletedGoalAnswer = Microsoft.JSInterop.Json.Deserialize<GoalAnswer>(await deleteResponseMessage.Content.ReadAsStringAsync());
                        Answer = new GoalAnswer
                        {
                            GoalKey = deletedGoalAnswer.GoalKey,
                            Date = deletedGoalAnswer.Date,
                            Notes = deletedGoalAnswer.Notes,
                        };
                    }
                }
                else
                {
                    Console.Write($"Going to put, GoalAnswer: {Answer.Answer}");
                    var response = await _httpClient.PutJsonAsync<GoalAnswer>("/api/GoalAnswer/GoalAnswer", Answer);
                    Answer.GoalAnswerKey = response.GoalAnswerKey;
                }
            }
        }    
    }
}