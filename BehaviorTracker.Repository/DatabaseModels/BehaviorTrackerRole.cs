using System.Collections.Generic;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerRole
    {
        public long BehaviorTrackerRoleKey { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<BehaviorTrackerRoleGroupRole> BehaviorTrackerRoleGroupRoles { get; set; }
    }
}