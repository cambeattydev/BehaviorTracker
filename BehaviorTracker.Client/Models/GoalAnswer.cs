using System;

namespace BehaviorTracker.Client.Models
{
    public class GoalAnswer
    {
        public long GoalAnswerKey { get; set; }
        public long GoalKey { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public Goal Goal { get; set; }
    }
}