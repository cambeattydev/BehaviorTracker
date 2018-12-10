using System.Collections.Generic;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Service.Models
{
    public class Goal
    {
        public long GoalKey { get; set; }
        public long StudentKey { get; set; }
        public string GoalDescription { get; set; }
        public GoalType GoalType { get; set; }
        public Student Student { get; set; }
        public IEnumerable<GoalAnswer> GoalAnswers { get; set; }
        public IEnumerable<GoalAvailableAnswer> AvailableAnswers { get; set; }
    }
}