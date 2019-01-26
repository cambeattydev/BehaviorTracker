using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Repository.Implementations
{
    public class GoalAvailableAnswerRepository : BaseRepository,  IGoalAvailableAnswerRepository
    {

        public GoalAvailableAnswerRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext) : base(behaviorTrackerDatabaseContext)
        {
        }
        public async Task<IEnumerable<GoalAvailableAnswer>> DeleteAllForGoalAsync(long goalKey)
        {
            var goalAvailableAnswersForGoalKey =
                _behaviorTrackerDatabaseContext.GoalAvailableAnswer.Where(goalAvailableAnswer =>
                    goalAvailableAnswer.GoalKey == goalKey);
            _behaviorTrackerDatabaseContext.GoalAvailableAnswer.RemoveRange(goalAvailableAnswersForGoalKey);
            await _behaviorTrackerDatabaseContext.SaveChangesAsync().ConfigureAwait(false);
            return goalAvailableAnswersForGoalKey;
        }

        public async Task<IEnumerable<GoalAvailableAnswer>> InsertAsync(IList<GoalAvailableAnswer> goalAvailableAnswers)
        {
            if (goalAvailableAnswers == null)
            {
                throw  new ArgumentNullException(nameof(goalAvailableAnswers));
            }
            //Do this so we only save the goalAvailableAnswers and not also the goal
            foreach (var goalAvailableAnswer in goalAvailableAnswers)
            {
                goalAvailableAnswer.Goal = null;
            }
            var addAvailableAnswersTask = _behaviorTrackerDatabaseContext.GoalAvailableAnswer.AddRangeAsync(goalAvailableAnswers);

            var saveChangesTask = _behaviorTrackerDatabaseContext.SaveChangesAsync();

            await Task.WhenAll(addAvailableAnswersTask, saveChangesTask).ConfigureAwait(false);
            
            return goalAvailableAnswers;




        }
    }
}