using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using BehaviorTracker.Client.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BehaviorTracker.Client.Shared.Common
{
    public class GoalAnswerComponent : BlazorComponent
    {
        [Parameter]
        protected Goal Goal { get; set; } 
        [Parameter]
        protected GoalAnswer Answer { get; set; } 
        
        [Parameter]
        protected DateTime CurrentDateTime { get; set; } 
        [Inject] HttpClient _httpClient { get; set; }

        protected GoalAnswerTotal GoalAnswerTotal;

        protected override async Task OnParametersSetAsync()
        {
            await GetGoalAnswerTotal();
        }

        private async Task GetGoalAnswerTotal()
        {
            GoalAnswerTotal = await _httpClient.GetJsonAsync<GoalAnswerTotal>(
                $"/api/GoalAnswer/GoalAnswerTotal/{Goal.GoalKey}/{HttpUtility.UrlEncode(CurrentDateTime.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))}");
        }
        protected async Task SaveAnswer()
        {
            if (Answer.GoalAnswerKey < 1 )
            {
                if (string.IsNullOrWhiteSpace(Answer.Answer)) return;

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
            await GetGoalAnswerTotal();
        }    
    }
}