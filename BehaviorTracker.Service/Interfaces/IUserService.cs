using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Service.Models;
using Microsoft.AspNetCore.Authentication;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<string>> LoginUserAsync(AuthenticateResult authenticationResult);
        Task<AuthorizationModel> GetUserRoles(string email);
    }
}