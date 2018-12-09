using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BehaviorTracker.Repository
{
    public class Goal
    {
        [Key]
        public long GoalKey { get; set; }
        public long StudentKey { get; set; }
        public string GoalDescription { get; set; }
        public int GoalType { get; set; }
        public Student Student { get; set; }
        [ForeignKey("GoalKey")]
        public ICollection<GoalAnswer> GoalAnswers { get; set; }
        [ForeignKey("GoalKey")]
        public ICollection<GoalAvailableAnswer> AvailableAnswers { get; set; }
    }
}