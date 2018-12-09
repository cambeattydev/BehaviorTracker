using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviorTracker.Repository
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