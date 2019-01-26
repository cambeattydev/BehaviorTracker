using System.Threading.Tasks;
using BehaviorTracker.Repository.Models;

namespace BehaviorTracker.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<BehaviorTrackerUser> GetUserAsync(string email);
    }
}