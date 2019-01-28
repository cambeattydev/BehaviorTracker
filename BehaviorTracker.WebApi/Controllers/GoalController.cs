using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GoalController : Controller
    {
        private readonly IGoalService _goalService;
        private readonly IMapper _mapper;

        public GoalController(IGoalService goalService, IMapper mapper)
        {
            _goalService = goalService;
            _mapper = mapper;
        }
        
        [HttpDelete("[action]/{goalKey}")]
        [Authorize(Roles =  nameof(BehaviorTrackerRoles.GoalWrite))]
        public async Task<IActionResult> Delete(long goalKey)
        {
            var deletedGoal = await _goalService.DeleteAsync(goalKey).ConfigureAwait(false);
            if (deletedGoal == null)
            {
                return BadRequest();
            }

            var mappedDeletedGoal = _mapper.Map<Client.Models.Goal>(deletedGoal);
            return Ok(mappedDeletedGoal);
        }
        
        [HttpPost("[action]")]
        [Authorize(Roles =  nameof(BehaviorTrackerRoles.GoalWrite))]
        public async Task<IActionResult> Goal([FromBody] Client.Models.Goal goalModel)
        {
            
            var goal = _mapper.Map<Service.Models.Goal>(goalModel);
            if (goal == null)
            {
                return BadRequest();
            }

            var savedGoal  = await _goalService.SaveAsync(goal).ConfigureAwait(false);
            _mapper.Map<Client.Models.Goal>(savedGoal);
            return Ok(savedGoal);
        }
    }
}