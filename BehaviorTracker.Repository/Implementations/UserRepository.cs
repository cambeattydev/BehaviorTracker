using System.Threading.Tasks;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Repository.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext) : base(behaviorTrackerDatabaseContext)
        {
        }

        public async Task<BehaviorTrackerUser> GetUserAsync(string email)
        {
            return await _behaviorTrackerDatabaseContext.BehaviorTrackerUsers.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}