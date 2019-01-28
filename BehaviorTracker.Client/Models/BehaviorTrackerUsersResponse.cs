using System;
using System.Collections.Generic;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Client.Models
{
    public class BehaviorTrackerUsersResponse : Copyable<BehaviorTrackerUsersResponse>
    {
        public BehaviorTrackerUser User { get; set; }

        public BehaviorTrackerRoleGroups RoleGroup { get; set; }

        public IEnumerable<BehaviorTrackerRoles> Roles { get; set; }
        
        public bool Editing { get; set; }
        
    }
}