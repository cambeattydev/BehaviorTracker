using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IUserService
    {
        Task<BehaviorTrackerUser> GetUserAsync(string email);
        Task<BehaviorTrackerUser> CreateUserAsync(BehaviorTrackerUser user);
        Task<IEnumerable<string>> GetUserRolesAsync(long behaviorTrackerUserKey);
    }
}