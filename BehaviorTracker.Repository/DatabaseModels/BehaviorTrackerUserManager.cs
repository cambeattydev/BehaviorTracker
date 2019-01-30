using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUserManager
    {
        [Key] public long BehaviorTrackerUserManagerKey { get; set; }

        public long ManagerBehaviorTrackerUserKey { get; set; }

        public long BehaviorTrackerUserKey { get; set; }

//        [ForeignKey("BehaviorTrackerUserKey")]
        public BehaviorTrackerUser ManagerBehaviorTrackerUser { get; set; }

//        [ForeignKey("BehaviorTrackerUserKey")]
        public BehaviorTrackerUser BehaviorTrackerUser { get; set; }
    }
}