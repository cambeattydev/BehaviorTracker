using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Repository.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext) : base(
            behaviorTrackerDatabaseContext)
        {
        }

        public async Task<BehaviorTrackerUser> GetUserAsync(string email)
        {
            return await _behaviorTrackerDatabaseContext.BehaviorTrackerUsers.FirstOrDefaultAsync(user =>
                user.Email == email);
        }

        public async Task<BehaviorTrackerUser> SaveUserAsync(BehaviorTrackerUser user)
        {
            var savedUser = user.BehaviorTrackerUserKey > 0
                ? _behaviorTrackerDatabaseContext.BehaviorTrackerUsers.Update(user).Entity
                : (await _behaviorTrackerDatabaseContext.BehaviorTrackerUsers.AddAsync(user)).Entity;

            await _behaviorTrackerDatabaseContext.SaveChangesAsync();
            return savedUser;
        }

        public  async Task<IEnumerable<BehaviorTrackerRole>> GetUserRolesAsync(long behaviorTrackerUserKey)
        {
            return await _behaviorTrackerDatabaseContext.BehaviorTrackerUserRoles.Where(userRoles =>
                    userRoles.BehaviorTrackerUserKey == behaviorTrackerUserKey)
                .Select(userRoles => userRoles.BehaviorTrackerRole).ToListAsync();
        }
    }
}