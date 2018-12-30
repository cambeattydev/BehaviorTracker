using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}