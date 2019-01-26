using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository) : base(mapper)
        {
            _userRepository = userRepository;
        }
        
        public async  Task<BehaviorTrackerUser> GetUserAsync(string email)
        {
            var repositoryUser = await _userRepository.GetUserAsync(email);
            return _mapper.Map<BehaviorTrackerUser>(repositoryUser);
        }

        public async Task<BehaviorTrackerUser> CreateUserAsync(BehaviorTrackerUser user)
        {
            var repositoryUser = _mapper.Map<Repository.Models.BehaviorTrackerUser>(user);
            var savedUser = await _userRepository.SaveUserAsync(repositoryUser);
            var mappedSavedUser = _mapper.Map<BehaviorTrackerUser>(savedUser);
            return mappedSavedUser;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(long behaviorTrackerUserKey)
        {
            return (await _userRepository.GetUserRolesAsync(behaviorTrackerUserKey)).Select(role => role.RoleName);
        }
    }
}