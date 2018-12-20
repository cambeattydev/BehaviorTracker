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
        
    }
}