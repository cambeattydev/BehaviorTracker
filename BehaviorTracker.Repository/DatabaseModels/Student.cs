using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class Student
    {
        [Key] public long StudentKey { get; set; }

        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public ICollection<Goal> Goals { get; set; }
    }
}