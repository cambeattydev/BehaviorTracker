using System.ComponentModel.DataAnnotations;

namespace BehaviorTracker.Repository.Models
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
    }
}