using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviorTracker.Repository.DatabaseModels
{
    public class BehaviorTrackerUser
    {
        [Key]
        public long BehaviorTrackerUserKey { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public IEnumerable<BehaviorTrackerUserRole> BehaviorTrackerUserRoles { get; set; }
    }
}