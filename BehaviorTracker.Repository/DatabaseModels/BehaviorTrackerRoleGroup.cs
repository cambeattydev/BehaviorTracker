using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerRoleGroup
    {
        [Key] public long BehaviorTrackerRoleGroupKey { get; set; }

        [Required] public string RoleGroupName { get; set; }

        public IEnumerable<BehaviorTrackerRoleGroupRole> RoleGroupRoles { get; set; }
    }
}