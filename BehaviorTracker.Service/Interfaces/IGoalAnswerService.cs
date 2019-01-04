using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Models;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IGoalAnswerService
    {
        Task<IDictionary<long, Models.GoalAnswer>> GetStudentGoalAnswers(long studentKey, DateTime dateTime);
        Task<Service.Models.GoalAnswer> SaveAsync(Service.Models.GoalAnswer goalAnswer);
    }
}