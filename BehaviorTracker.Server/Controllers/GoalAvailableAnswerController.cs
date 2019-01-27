using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class GoalAvailableAnswerController : Controller
    {
        private readonly IGoalAvailableAnswerService _goalAvailableAnswerService;
        private readonly IMapper _mapper;

        public GoalAvailableAnswerController(IGoalAvailableAnswerService goalAvailableAnswerService, IMapper mapper)
        {
            _goalAvailableAnswerService = goalAvailableAnswerService;
            _mapper = mapper;
        }
        
        [HttpDelete("[action]/{goalKey}")]
        [Authorize(Roles =  nameof(BehaviorTrackerRoles.GoalWrite))]
        public async Task<IActionResult> DeleteAllForGoal(long goalKey)
        {
            var deletedGoalAvailableAnswers = await _goalAvailableAnswerService.DeleteAllForGoal(goalKey).ConfigureAwait(false);

            var mappedDeletedGoalAvailableAnswers = deletedGoalAvailableAnswers.Select(_mapper.Map<Client.Models.GoalAvailableAnswer>);
            return Ok(mappedDeletedGoalAvailableAnswers);
        }
        
        [HttpPost("[action]")]
        [Authorize(Roles =  nameof(BehaviorTrackerRoles.GoalWrite))]
        public async Task<IActionResult> DeleteAndInsert([FromBody] IEnumerable<Client.Models.GoalAvailableAnswer> goalAvailableAnswers)
        {
            
            var mappedAvailableAnswers = goalAvailableAnswers?.Select(_mapper.Map<Service.Models.GoalAvailableAnswer>);
            if (mappedAvailableAnswers == null)
            {
                return BadRequest();
            }

            var savedAvailableAnswers  = await _goalAvailableAnswerService.DeleteAndInsertAsync(mappedAvailableAnswers).ConfigureAwait(false);
            return Ok(savedAvailableAnswers.Select(_mapper.Map<Client.Models.GoalAvailableAnswer>));

        }
    }
}