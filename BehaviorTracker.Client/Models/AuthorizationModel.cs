using System.Collections.Generic;

namespace BehaviorTracker.Client.Models
{
    public class AuthorizationModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public IList<string> Roles { get; set; }
        
    }
}