using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUserRoles
    {
        public long BehaviorTrackerUserKey { get; set; }
        
        public long BehaviorTrackerRoleKey { get; set; }
        
        [ForeignKey("BehaviorTrackerUserKey")]
        public virtual BehaviorTrackerUser BehaviorTrackerUser { get; set; }
        
        [ForeignKey("BehaviorTrackerRoleKey")]
        public virtual BehaviorTrackerRole BehaviorTrackerRole { get; set; }
    }
}