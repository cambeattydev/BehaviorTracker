using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerRoleGroupRole
    {
        [Key]
        public long BehaviorTrackerRoleGroupRoleKey { get; set; }
        
        public long BehaviorTrackerRoleGroupKey { get; set; }
        
        public long BehaviorTrackerRoleKey { get; set; }
        
        [ForeignKey("BehaviorTrackerRoleKey")]
        public BehaviorTrackerRole BehaviorTrackerRole { get; set; }
        
        [ForeignKey("BehaviorTrackerRoleGroupKey")]
        public BehaviorTrackerRoleGroup BehaviorTrackerRoleGroup { get; set; }
    }
}