using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using BehaviorTracker.Shared;
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
                goalAnswer.Goal = null;
                if (goalAnswer.GoalAnswerKey < 1)
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

        public IEnumerable<GoalAnswerScore> GoalAnswersScore(long goalKey, DateTime date)
        {
            try
            {
                return _behaviorTrackerDatabaseContext.GoalAnswers.Where(goalAnswer =>
                    goalAnswer.GoalKey == goalKey && goalAnswer.Date.Date == date.Date).Select(goalAnswer =>
                    new GoalAnswerScore
                    {
                        MaxValue = goalAnswer.Goal.GoalType == GoalType.Numeric
                            ? goalAnswer.Goal.AvailableAnswers.Max(availableAnswer =>
                                float.Parse(availableAnswer.OptionValue))
                            : 1,
                        GoalType = goalAnswer.Goal.GoalType,
                        Goal = goalAnswer
                    }).AsEnumerable();
            }
            catch (Exception e)
            {
                var test = e;
                throw;
            }
        }
        
        public IEnumerable<GoalAnswerScore> WeeklyGoalAnswersScore(long goalKey, DateTime mondayDate)
        {
            try
            {
                return _behaviorTrackerDatabaseContext.GoalAnswers.Where(goalAnswer =>
                    goalAnswer.GoalKey == goalKey && goalAnswer.Date.Date >= mondayDate.Date && goalAnswer.Date.Date <= mondayDate.Date.AddDays(7)).Select(goalAnswer =>
                    new GoalAnswerScore
                    {
                        MaxValue = goalAnswer.Goal.GoalType == GoalType.Numeric
                            ? goalAnswer.Goal.AvailableAnswers.Max(availableAnswer =>
                                float.Parse(availableAnswer.OptionValue))
                            : 1,
                        GoalType = goalAnswer.Goal.GoalType,
                        Goal = goalAnswer
                    }).AsEnumerable();
            }
            catch (Exception e)
            {
                var test = e;
                throw;
            }
        }
    }
}