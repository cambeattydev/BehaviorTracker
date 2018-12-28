using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IGoalAvailableAnswerService
    {
        Task<IEnumerable<GoalAvailableAnswer>> DeleteAndInsertAsync(IEnumerable<GoalAvailableAnswer> goalAvailableAnswers);
        Task<IEnumerable<GoalAvailableAnswer>> DeleteAllForGoal(long goalKey);
    }
}