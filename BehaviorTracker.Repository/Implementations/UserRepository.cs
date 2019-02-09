using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.DatabaseModels;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.OtherModels;
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
//            _behaviorTrackerDatabaseContext.BehaviorTrackerUserRoleGroups.Add(new BehaviorTrackerUserRoleGroup
//            {
//                BehaviorTrackerUserKey = 2,
//                BehaviorTrackerRoleGroupKey = 1,
//                BehaviorTrackerUserRoleGroupKey = 2
//            });
//            await _behaviorTrackerDatabaseContext.SaveChangesAsync();
            var savedUser = user.BehaviorTrackerUserKey > 0
                ? _behaviorTrackerDatabaseContext.BehaviorTrackerUsers.Update(user).Entity
                : (await _behaviorTrackerDatabaseContext.BehaviorTrackerUsers.AddAsync(user)).Entity;

            await _behaviorTrackerDatabaseContext.SaveChangesAsync();
            return savedUser;
        }

        public async Task<IEnumerable<BehaviorTrackerRole>> GetUserRolesAsync(long behaviorTrackerUserKey)
        {
            return await _behaviorTrackerDatabaseContext.BehaviorTrackerUserRoleGroups.Where(userRoles =>
                    userRoles.BehaviorTrackerUserKey == behaviorTrackerUserKey)
                .Where(userRoles => userRoles.BehaviorTrackerRoleGroup != null &&
                                    userRoles.BehaviorTrackerRoleGroup.BehaviorTrackerRoleGroupRoles != null)
                .SelectMany(userRoles =>
                    userRoles.BehaviorTrackerRoleGroup.BehaviorTrackerRoleGroupRoles.Select(roleGroupRole =>
                        roleGroupRole.BehaviorTrackerRole)).ToListAsync();
        }

        public IEnumerable<BehaviorTrackerUsersResponse> GetUsers()
        {
            return _behaviorTrackerDatabaseContext.BehaviorTrackerUsers
                .Select(user => new BehaviorTrackerUsersResponse
                {
                    User = user,
                    Roles = user.BehaviorTrackerUserRoleGroup.BehaviorTrackerRoleGroup.BehaviorTrackerRoleGroupRoles
                        .Select(
                            roleGroupRole => roleGroupRole.BehaviorTrackerRole),
                    RoleGroup = user.BehaviorTrackerUserRoleGroup.BehaviorTrackerRoleGroup
                })
                .AsEnumerable();
        }

        public async Task<BehaviorTrackerRoleGroup> GetRoleGroupAsync(long behaviorTrackerUserKey)
        {
            return (await _behaviorTrackerDatabaseContext.BehaviorTrackerUsers
                    .Include(user => user.BehaviorTrackerUserRoleGroup)
                    .ThenInclude(userRoleGroup => userRoleGroup.BehaviorTrackerRoleGroup)
                    .FirstOrDefaultAsync(user => user.BehaviorTrackerUserKey == behaviorTrackerUserKey))
                ?.BehaviorTrackerUserRoleGroup?.BehaviorTrackerRoleGroup;
        }
    }
}