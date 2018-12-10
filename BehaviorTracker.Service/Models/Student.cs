using System.Collections.Generic;

namespace BehaviorTracker.Service.Models
{
    public class Student
    {
        public long StudentKey { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
    }
}