using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Repository.DatabaseModels;

namespace BehaviorTracker.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<BehaviorTrackerUser> GetUserAsync(string email);
        Task<BehaviorTrackerUser> SaveUserAsync(BehaviorTrackerUser user);
        Task<IEnumerable<BehaviorTrackerRole>> GetUserRolesAsync(long behaviorTrackerUserKey);
    }
}