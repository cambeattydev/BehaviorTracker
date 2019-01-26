using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using Microsoft.AspNetCore.Authentication;

namespace BehaviorTracker.Service.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<string>> LoginUserAsync(AuthenticateResult authenticationResult)
        {
            var user = await GetUserAsync(authenticationResult.Principal.Claims.FirstOrDefault(claim =>
                claim.Type == ClaimTypes.Email)?.Value);

            if (user == null)
            {
                //Create new user for the login
                var userToBeSaved = new BehaviorTrackerUser
                {
                    Email = authenticationResult.Principal.Claims
                        .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value,
                    FirstName = authenticationResult.Principal.Claims
                        .FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value,
                    LastName = authenticationResult.Principal.Claims
                        .FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value
                };
                user = await CreateUserAsync(userToBeSaved);
            }

            return  await GetUserRolesAsync(user.BehaviorTrackerUserKey);
        }

        public async Task<AuthorizationModel> GetUserRoles(string email)
        {
            var user = await GetUserAsync(email);
            if (user == null)
            {
                return null;
            }
            var roles = await GetUserRolesAsync(user.BehaviorTrackerUserKey);
            return new AuthorizationModel
            {
                User = user,
                Roles = roles.ToList()
            };
        }
        
        private async  Task<BehaviorTrackerUser> GetUserAsync(string email)
        {
            var repositoryUser = await _userRepository.GetUserAsync(email);
            return _mapper.Map<BehaviorTrackerUser>(repositoryUser);
        }

        private async Task<BehaviorTrackerUser> CreateUserAsync(BehaviorTrackerUser user)
        {
            var repositoryUser = _mapper.Map<Repository.DatabaseModels.BehaviorTrackerUser>(user);
            var savedUser = await _userRepository.SaveUserAsync(repositoryUser);
            var mappedSavedUser = _mapper.Map<BehaviorTrackerUser>(savedUser);
            return mappedSavedUser;
        }

        private async Task<IEnumerable<string>> GetUserRolesAsync(long behaviorTrackerUserKey)
        {
            return (await _userRepository.GetUserRolesAsync(behaviorTrackerUserKey)).Select(role => role.RoleName);
        }
    }
}