using System.Threading.Tasks;
using BehaviorTracker.Repository.DatabaseModels;
using BehaviorTracker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Repository.Implementations
{
    public class GoalRepository : BaseRepository, IGoalRepository
    {
        public GoalRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext) : base(
            behaviorTrackerDatabaseContext)
        {
        }

        public async Task<Goal> DeleteAsync(long goalKey)
        {
            var goalToDelete = await _behaviorTrackerDatabaseContext.Goals
                .FirstOrDefaultAsync(goal => goal.GoalKey == goalKey).ConfigureAwait(false);
            if (goalToDelete != null)
            {
                _behaviorTrackerDatabaseContext.Goals.Remove(goalToDelete);
                await _behaviorTrackerDatabaseContext.SaveChangesAsync().ConfigureAwait(false);

                return goalToDelete;
            }

            return null;
        }

        public async Task<Goal> SaveAsync(Goal goal)
        {
            var savedGoal = goal.GoalKey < 1
                ? _behaviorTrackerDatabaseContext.Goals.Add(goal)
                : _behaviorTrackerDatabaseContext.Goals.Update(goal);

            await _behaviorTrackerDatabaseContext.SaveChangesAsync().ConfigureAwait(false);
            return savedGoal.Entity;
        }
    }
}