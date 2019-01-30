using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUser
    {
        [Key] public long BehaviorTrackerUserKey { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        public BehaviorTrackerUserRoleGroup BehaviorTrackerUserRoleGroup { get; set; }

//        [ForeignKey("BehaviorTrackerUserKey")]
        public IEnumerable<BehaviorTrackerUserManager> BehaviorTrackerUserManagers { get; set; }

//        [ForeignKey("ManagerBehaviorTrackerUserKey")]
        public ICollection<BehaviorTrackerUserManager> MangedBehaviorTrackerUsers { get; set; }

        public ICollection<Goal> Goals { get; set; }
    }
}