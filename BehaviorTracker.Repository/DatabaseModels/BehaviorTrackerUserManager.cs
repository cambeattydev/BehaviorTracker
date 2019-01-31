namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUserManager
    {
        public long BehaviorTrackerUserManagerKey { get; set; }
        public long ManagerBehaviorTrackerUserKey { get; set; }
        public long BehaviorTrackerUserKey { get; set; }
        public BehaviorTrackerUser ManagerBehaviorTrackerUser { get; set; }
        public BehaviorTrackerUser BehaviorTrackerUser { get; set; }
    }
}