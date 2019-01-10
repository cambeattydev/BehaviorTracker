using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviorTracker.Client.Models
{
    public class GoalAvailableAnswerEditModel : Copyable<GoalAvailableAnswerEditModel>
    {
        public long GoalKey { get; set; }
        public float StartValue { get; set; }
        public float EndValue { get; set; }
        public float StepAmount { get; set; }
        
        public GoalAvailableAnswerEditModel(){}
        public GoalAvailableAnswerEditModel(Goal goal)
        {
            var parsedOptions = new List<float>();
            if (goal.AvailableAnswers != null)
            {
                parsedOptions.AddRange(goal.AvailableAnswers.Select(s =>
                    float.TryParse(s.OptionValue, out var parsedInt) ? parsedInt : 0));
                parsedOptions = parsedOptions.OrderBy(s => s).ToList();
            }

            StartValue = parsedOptions.Any() ? parsedOptions.Min() : 0;
            EndValue = parsedOptions.Any() ? parsedOptions.Max() : 0;
            StepAmount = parsedOptions.Count() > 1
                ? Math.Abs(parsedOptions.FirstOrDefault() - parsedOptions.ElementAtOrDefault(1))
                : 0;
            GoalKey = goal.GoalKey;

        }
        
    }
}