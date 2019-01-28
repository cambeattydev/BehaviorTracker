using System.Collections.Generic;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Service.Models
{
    public class AuthorizationModel
    {
        public BehaviorTrackerUser User { get; set; }

        public IList<string> Roles { get; set; }
        public BehaviorTrackerRoleGroups RoleGroup { get; set; }
    }
}