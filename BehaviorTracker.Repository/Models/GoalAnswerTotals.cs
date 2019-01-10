using BehaviorTracker.Shared;

namespace BehaviorTracker.Repository.Models
{
    public class GoalAnswerTotals
    {
        public float MaxValue { get; set; }
        
        public GoalType GoalType { get; set; }

        public GoalAnswer Goal { get; set; }
    }
}