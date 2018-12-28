using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.Models
{
    public class GoalAvailableAnswer
    {
        [Key]
        public long GoalAvailableAnswerKey { get; set; }
        public long GoalKey { get; set; }
        public string OptionValue { get; set; }
        
        public Goal Goal { get; set; }
    }
}