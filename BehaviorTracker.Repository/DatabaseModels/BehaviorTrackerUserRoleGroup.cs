using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUserRoleGroup
    {
        [Key]
        public long BehaviorTrackerUserRoleGroupKey { get; set; }
        
        public long BehaviorTrackerUserKey { get; set; }
        
        public long BehaviorTrackerRoleGroupKey { get; set; }
        
        [ForeignKey("BehaviorTrackerUserKey")]
        public BehaviorTrackerUser BehaviorTrackerUser { get; set; }
        [ForeignKey("BehaviorTrackerRoleGroupKey")]
        public BehaviorTrackerRoleGroup BehaviorTrackerRoleGroup { get; set; }
    }
}