using System.Collections.Generic;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUser
    {
        public long BehaviorTrackerUserKey { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BehaviorTrackerUserRoleGroup BehaviorTrackerUserRoleGroup { get; set; }
        public IEnumerable<BehaviorTrackerUserManager> BehaviorTrackerUserManagers { get; set; }
        public ICollection<BehaviorTrackerUserManager> MangedBehaviorTrackerUsers { get; set; }
        public ICollection<Goal> Goals { get; set; }
    }
}