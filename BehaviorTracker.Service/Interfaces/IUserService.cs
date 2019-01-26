using System.Threading.Tasks;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IUserService
    {
        Task<BehaviorTrackerUser> GetUserAsync(string email);
    }
}