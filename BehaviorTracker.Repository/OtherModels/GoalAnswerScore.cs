using BehaviorTracker.Repository.DatabaseModels;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Repository.OtherModels
{
    public class GoalAnswerScore
    {
        public float MaxValue { get; set; }
        
        public GoalType GoalType { get; set; }

        public GoalAnswer Goal { get; set; }
    }
}