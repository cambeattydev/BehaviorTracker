using System.Collections.Generic;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerRoleGroup
    {
        public long BehaviorTrackerRoleGroupKey { get; set; }

        public string RoleGroupName { get; set; }

        public IEnumerable<BehaviorTrackerRoleGroupRole> BehaviorTrackerRoleGroupRoles { get; set; }

        public IEnumerable<BehaviorTrackerUserRoleGroup> BehaviorTrackerUserRoleGroups { get; set; }
    }
}