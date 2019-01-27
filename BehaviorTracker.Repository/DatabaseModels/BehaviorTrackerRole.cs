using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerRole
    { 
        [Key]
        public long BehaviorTrackerRoleKey { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}