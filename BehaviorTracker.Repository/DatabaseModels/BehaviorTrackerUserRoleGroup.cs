namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUserRoleGroup
    {
        public long BehaviorTrackerUserRoleGroupKey { get; set; }
        public long BehaviorTrackerUserKey { get; set; }
        public long BehaviorTrackerRoleGroupKey { get; set; }
        public virtual BehaviorTrackerUser BehaviorTrackerUser { get; set; }
        public BehaviorTrackerRoleGroup BehaviorTrackerRoleGroup { get; set; }
    }
}