using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BehaviorTracker.Repository.OtherModels;
using BehaviorTracker.Service.Models;
using Microsoft.AspNetCore.Authentication;
using BehaviorTrackerUsersResponse = BehaviorTracker.Repository.OtherModels.BehaviorTrackerUsersResponse;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Claim>> LoginUserAsync(AuthenticateResult authenticationResult);
        Task<AuthorizationModel> GetUserRoles(string email);
        IEnumerable<Models.BehaviorTrackerUsersResponse> GetUsers();
    }
}