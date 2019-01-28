using System.Collections.Generic;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Client.Models
{
    public class AuthorizationModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        public BehaviorTrackerRoleGroups RoleGroup { get; set; }
    }
}