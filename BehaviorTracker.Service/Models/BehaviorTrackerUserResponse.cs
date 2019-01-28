using System.Collections.Generic;

namespace BehaviorTracker.Service.Models
{
    public class BehaviorTrackerUserResponse
    {
        public BehaviorTrackerUser User { get; set; }

        public BehaviorTrackerRoleGroup RoleGroup { get; set; }

        public IEnumerable<BehaviorTrackerRole> Roles { get; set; }
    }
}