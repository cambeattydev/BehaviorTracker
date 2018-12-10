namespace BehaviorTracker.Service.Models
{
    public class GoalAvailableAnswer
    {
        public long GoalAvailableAnswerKey { get; set; }
        public long GoalKey { get; set; }
        public string OptionValue { get; set; }
        public Goal Goal { get; set; }
    }
}