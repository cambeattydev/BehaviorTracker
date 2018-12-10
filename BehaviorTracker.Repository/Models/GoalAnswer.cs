using System;
using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.Models
{
    public class GoalAnswer
    {
        [Key]
        public long GoalAnswerKey { get; set; }
        public long GoalKey { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public Goal Goal { get; set; }
    }
}