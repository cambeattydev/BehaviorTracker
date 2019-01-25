using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.Models
{
    public class BehaviorTrackerRole
    { 
        [Key]
        public long BehaviorTrackerRoleKey { get; set; }
        
        public string RoleName { get; set; }
    }
}