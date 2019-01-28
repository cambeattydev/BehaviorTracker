using System.Collections.Generic;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Service.Models
{
    public class Goal
    {
        public long GoalKey { get; set; }
        public long BehaviorTrackerUserKey { get; set; }
        public string GoalDescription { get; set; }
        public GoalType GoalType { get; set; }
        public BehaviorTrackerUser BehaviorTrackerUser { get; set; }
        public IEnumerable<GoalAnswer> GoalAnswers { get; set; }
        public IEnumerable<GoalAvailableAnswer> AvailableAnswers { get; set; }
    }
}