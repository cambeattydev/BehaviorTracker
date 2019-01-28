using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.OtherModels;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using BehaviorTracker.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BehaviorTracker.Service.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private const string RoleGroupClaimType = "RoleGroup";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) :
            base(mapper)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Claim>> LoginUserAsync(AuthenticateResult authenticationResult)
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

            var roles = await GetUserRolesAsync(user.BehaviorTrackerUserKey);
            var roleGroup = await GetRoleGroup(user.BehaviorTrackerUserKey);

            return roles.Select(role =>
                    new Claim(ClaimTypes.Role, role))
                .Append(new Claim(RoleGroupClaimType, roleGroup.BehaviorTrackerRoleGroupKey.ToString()));
        }

        public async Task<AuthorizationModel> GetUserRoles(string email)
        {
            var user = await GetUserAsync(email);
            if (user == null)
            {
                return null;
            }

            var roles = await GetUserRolesAsync(user.BehaviorTrackerUserKey);
            var roleGroup = await GetRoleGroup(user.BehaviorTrackerUserKey);
            return new AuthorizationModel
            {
                User = user,
                Roles = roles.ToList(),
                RoleGroup = (BehaviorTrackerRoleGroups) roleGroup.BehaviorTrackerRoleGroupKey
            };
        }

        public IEnumerable<Models.BehaviorTrackerUsersResponse> GetUsers()
        {
            var allowedBehaviorTrackerRoleGroups = new[] {BehaviorTrackerRoleGroups.Admin, BehaviorTrackerRoleGroups.Teacher};
            var behaviorTrackerRoleGroup = GetBehaviorTrackerRoleGroup();

            if (!allowedBehaviorTrackerRoleGroups.Contains(behaviorTrackerRoleGroup)) return Enumerable.Empty<Models.BehaviorTrackerUsersResponse>();

            var repositoryUsersResponse = _userRepository.GetUsers();
            var mappedUsersResponse = repositoryUsersResponse.Select(_mapper.Map<Models.BehaviorTrackerUsersResponse>);
            // Only show users that are in groups bellow what the user currently has, unless admin
            var usersAllowedToSee = mappedUsersResponse.Where(user => behaviorTrackerRoleGroup == BehaviorTrackerRoleGroups.Admin ||
                               user.RoleGroup.BehaviorTrackerRoleGroupKey > (long) behaviorTrackerRoleGroup);
            return usersAllowedToSee;
        }

        private async Task<BehaviorTrackerUser> GetUserAsync(string email)
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

        public async Task<BehaviorTrackerRoleGroup> GetRoleGroup(long behaviorTrackerUserKey)
        {
            var roleGroup = await _userRepository.GetRoleGroupAsync(behaviorTrackerUserKey);
            return _mapper.Map<BehaviorTrackerRoleGroup>(roleGroup);
        }

        private BehaviorTrackerRoleGroups GetBehaviorTrackerRoleGroup()
        {
            var roleGroupClaimString = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == RoleGroupClaimType)?.Value;
            if (string.IsNullOrWhiteSpace(roleGroupClaimString))
            {
                return BehaviorTrackerRoleGroups.None;
            }

            var result = Enum.TryParse(roleGroupClaimString, out BehaviorTrackerRoleGroups behaviorTrackerRoleGroup);
            return result ? behaviorTrackerRoleGroup : BehaviorTrackerRoleGroups.None;
        }
    }
}