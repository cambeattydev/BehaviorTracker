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
    }
}