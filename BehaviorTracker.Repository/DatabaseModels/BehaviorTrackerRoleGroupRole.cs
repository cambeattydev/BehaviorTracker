namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerRoleGroupRole
    {
        public long BehaviorTrackerRoleGroupRoleKey { get; set; }

        public long BehaviorTrackerRoleGroupKey { get; set; }

        public long BehaviorTrackerRoleKey { get; set; }

        public BehaviorTrackerRole BehaviorTrackerRole { get; set; }
        public BehaviorTrackerRoleGroup BehaviorTrackerRoleGroup { get; set; }
    }
}