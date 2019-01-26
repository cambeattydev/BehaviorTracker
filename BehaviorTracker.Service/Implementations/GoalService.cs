using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Service.Implementations
{
    public class GoalService : BaseService, IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        
        public GoalService(IMapper mapper, IGoalRepository goalRepository) :base(mapper)
        {
            _goalRepository = goalRepository;
        }

        public async Task<Goal> DeleteAsync(long goalKey)
        {
            var deletedGoal = await _goalRepository.DeleteAsync(goalKey).ConfigureAwait(false);
            var goal = _mapper.Map<Goal>(deletedGoal);
            return goal;
        }

        public async Task<Goal> SaveAsync(Goal goal)
        {
            var repositoryGoal = _mapper.Map<Repository.Models.Goal>(goal);
            var savedGoal = await _goalRepository.SaveAsync(repositoryGoal).ConfigureAwait(false);
            if (savedGoal.GoalType != GoalType.Numeric)
            {
                //Delete the goal available answers
            }
            return _mapper.Map<Goal>(savedGoal);
        }
    }
}