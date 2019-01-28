using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BehaviorTracker.Repository.OtherModels;
using BehaviorTracker.Service.Models;
using Microsoft.AspNetCore.Authentication;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Claim>> LoginUserAsync(AuthenticateResult authenticationResult);
        Task<AuthorizationModel> GetUserRoles(string email);
        IEnumerable<BehaviorTrackerUsersResponse> GetUsers();
    }
}