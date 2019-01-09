using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BehaviorTracker.Repository.Implementations
{
    public class GoalAnswerRepository : IGoalAnswerRepository
    {
        private readonly BehaviorTrackerDatabaseContext _behaviorTrackerDatabaseContext;
        public GoalAnswerRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext)
        {
            _behaviorTrackerDatabaseContext = behaviorTrackerDatabaseContext;
        }
        public async Task<IDictionary<long,GoalAnswer>> GetStudentGoalAnswers(long studentKey, DateTime dateTime)
        {
            return await _behaviorTrackerDatabaseContext.GoalAnswers
                .Where(goalAnswer => goalAnswer.Date == dateTime && goalAnswer.Goal.StudentKey == studentKey)
                .ToDictionaryAsync(goalAnswer => goalAnswer.GoalKey, goalAnswer => goalAnswer);
        }

        public async Task<GoalAnswer> SaveAsync(GoalAnswer goalAnswer)
        {
                EntityEntry<GoalAnswer> savedGoalAnswer = default(EntityEntry<GoalAnswer>);
                if (goalAnswer.GoalAnswerKey > 1)
                {
                    savedGoalAnswer = await _behaviorTrackerDatabaseContext.GoalAnswers.AddAsync(goalAnswer);
                }
                else
                {
                    savedGoalAnswer = _behaviorTrackerDatabaseContext.Update(goalAnswer);
                }

                await _behaviorTrackerDatabaseContext.SaveChangesAsync();
                return savedGoalAnswer.Entity;
        }

        /// <inheritdoc />
        public async Task<GoalAnswer> DeleteAsync(long goalAnswerKey)
        {
            var deletedGoalAnswer = await _behaviorTrackerDatabaseContext.GoalAnswers.FirstOrDefaultAsync(goalAnswer => goalAnswer.GoalAnswerKey == goalAnswerKey);

            if (deletedGoalAnswer == null) return null;

            _behaviorTrackerDatabaseContext.GoalAnswers.Remove(deletedGoalAnswer);
            await _behaviorTrackerDatabaseContext.SaveChangesAsync();
            return deletedGoalAnswer;
        }
    }
}