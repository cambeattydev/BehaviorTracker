using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Models;
using BehaviorTracker.Service.Models;
using GoalAnswerScore = BehaviorTracker.Service.Models.GoalAnswerScore;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IGoalAnswerService
    {
        Task<IDictionary<long, Models.GoalAnswer>> GetStudentGoalAnswers(long studentKey, DateTime dateTime);
        Task<Service.Models.GoalAnswer> SaveAsync(Service.Models.GoalAnswer goalAnswer);
        Task<Service.Models.GoalAnswer> DeleteAsync(long goalAnswerKey);
        GoalAnswerTotal GoalAnswersTotal(long goalKey, DateTime date);
    }
}