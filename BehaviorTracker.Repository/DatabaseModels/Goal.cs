using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class Goal
    {
        [Key]
        public long GoalKey { get; set; }
        public long BehaviorTrackerUserKey { get; set; }
        public string GoalDescription { get; set; }
        public GoalType GoalType { get; set; }
        public BehaviorTrackerUser BehaviorTrackerUser { get; set; }
        [ForeignKey("GoalKey")]
        public ICollection<GoalAnswer> GoalAnswers { get; set; }
        [ForeignKey("GoalKey")]
        public ICollection<GoalAvailableAnswer> AvailableAnswers { get; set; }
    }
}