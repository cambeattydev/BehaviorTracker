using System.Threading.Tasks;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Repository.Implementations
{
    public class GoalRepository : IGoalRepository
    {
        private readonly BehaviorTrackerDatabaseContext _dbContext;

        public GoalRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext)
        {
            _dbContext = behaviorTrackerDatabaseContext;
        }

        public async Task<Goal> DeleteAsync(long goalKey)
        {
            var goalToDelete = await _dbContext.Goals.FirstOrDefaultAsync(goal => goal.GoalKey == goalKey).ConfigureAwait(false);
            if (goalToDelete != null)
            {
                _dbContext.Goals.Remove(goalToDelete);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return goalToDelete;
            }

            return null;
        }

        public async Task<Goal> SaveAsync(Goal goal)
        {
            var savedGoal = goal.GoalKey < 1 ?
                _dbContext.Goals.Add(goal) : 
                _dbContext.Goals.Update(goal);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return savedGoal.Entity;
        }
    }
}