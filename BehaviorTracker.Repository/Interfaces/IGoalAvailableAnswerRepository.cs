using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Repository.DatabaseModels;

namespace BehaviorTracker.Repository.Interfaces
{
    public interface IGoalAvailableAnswerRepository
    {
        Task<IEnumerable<GoalAvailableAnswer>> DeleteAllForGoalAsync(long goalKey);
        Task<IEnumerable<GoalAvailableAnswer>> InsertAsync(IList<GoalAvailableAnswer> goalAvailableAnswers);
    }
}