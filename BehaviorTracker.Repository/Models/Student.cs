using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviorTracker.Repository.Models
{
    public class Student
    {
        [Key]
        public long StudentKey { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        [ForeignKey("StudentKey")]
        public ICollection<Goal> Goals { get; set; }
    }
}