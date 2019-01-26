using System.Threading.Tasks;
using BehaviorTracker.Repository.DatabaseModels;

namespace BehaviorTracker.Repository.Interfaces
{
    public interface IGoalRepository
    {
        Task<Goal> DeleteAsync(long goalKey);
        Task<Goal> SaveAsync(Goal repositoryStudent);
    }
}