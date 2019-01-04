using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Models;

namespace BehaviorTracker.Repository.Interfaces
{
    public interface IGoalAnswerRepository
    {
        Task<IDictionary<long, GoalAnswer>> GetStudentGoalAnswers(long studentKey, DateTime dateTime);
        Task<GoalAnswer> SaveAsync(GoalAnswer goalAnswer);
    }
}