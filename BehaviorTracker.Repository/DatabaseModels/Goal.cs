using System.Collections.Generic;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class Goal
    {
        public long GoalKey { get; set; }
        public long BehaviorTrackerUserKey { get; set; }
        public string GoalDescription { get; set; }
        public GoalType GoalType { get; set; }
        public BehaviorTrackerUser BehaviorTrackerUser { get; set; }
        public ICollection<GoalAnswer> GoalAnswers { get; set; }
        public ICollection<GoalAvailableAnswer> AvailableAnswers { get; set; }
    }
}