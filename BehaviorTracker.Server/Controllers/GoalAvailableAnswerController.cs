using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Service.Interfaces;
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
        public async Task<IActionResult> DeleteAllForGoal(long goalKey)
        {
            var deletedGoal = await _goalAvailableAnswerService.DeleteAllForGoal(goalKey).ConfigureAwait(false);
            if (deletedGoal == null)
            {
                return BadRequest();
            }

            var mappedDeletedGoal = _mapper.Map<Client.Models.Goal>(deletedGoal);
            return Ok(mappedDeletedGoal);
        }
        
        [HttpPost("[action]")]
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