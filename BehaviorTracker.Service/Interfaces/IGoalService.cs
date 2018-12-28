using System.Threading.Tasks;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IGoalService
    {
        Task<Goal> DeleteAsync(long goalKey);
        Task<Goal> SaveAsync(Goal goal);
    }
}