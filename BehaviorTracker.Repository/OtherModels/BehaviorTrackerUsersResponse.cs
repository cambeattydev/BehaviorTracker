using System.Collections.Generic;
using BehaviorTracker.Repository.DatabaseModels;

namespace BehaviorTracker.Repository.OtherModels
{
    public class BehaviorTrackerUsersResponse
    {
        public BehaviorTrackerUser User { get; set; }

        public BehaviorTrackerRoleGroup RoleGroup { get; set; }

        public IEnumerable<BehaviorTrackerRole> Roles { get; set; }
    }
}