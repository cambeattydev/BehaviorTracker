using System.Collections.Generic;

namespace BehaviorTracker.Service.Models
{
    public class AuthorizationModel
    {
        public BehaviorTrackerUser User { get; set; }
        
        public IList<string> Roles { get; set; }
    }
}