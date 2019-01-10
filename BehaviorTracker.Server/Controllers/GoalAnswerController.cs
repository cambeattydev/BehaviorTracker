using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Client.Models;
using BehaviorTracker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class GoalAnswerController : Controller
    {
        private readonly IGoalAnswerService _goalAnswerService;
        private readonly IMapper _mapper;

        public GoalAnswerController(IGoalAnswerService goalAnswerService, IMapper mapper)
        {
            _goalAnswerService = goalAnswerService;
            _mapper = mapper;
        }
        
        [HttpGet("[action]/{studentKey}/{datetime}")]
        public async Task<IActionResult> StudentGoalAnswers(long studentKey, DateTime dateTime)
        {
            var studentGoalAnswers = await _goalAnswerService.GetStudentGoalAnswers(studentKey, dateTime);
            if (studentGoalAnswers == null || studentGoalAnswers.Count < 1)
            {
                return NoContent();
            }

            return Ok(studentGoalAnswers.ToDictionary(goalAnswer => goalAnswer.Key,
                kvp => _mapper.Map<Client.Models.GoalAnswer>(kvp.Value)));

        }
        
        
        [HttpPost("[action]")]
        [HttpPut("[action]")]
        public async Task<IActionResult> GoalAnswer([FromBody] Client.Models.GoalAnswer goalAnswer)
        {
            var mappedGoalAnswer = _mapper.Map<Service.Models.GoalAnswer>(goalAnswer);
            var savedGoalAnswer = await _goalAnswerService.SaveAsync(mappedGoalAnswer);
            
            return Ok(_mapper.Map<Client.Models.GoalAnswer>(savedGoalAnswer));

        }

        [HttpDelete("[action]/{goalAnswerKey}")]
        public async Task<IActionResult> GoalAnswer(long goalAnswerKey)
        {
            var deletedGoalAnswer = await _goalAnswerService.DeleteAsync(goalAnswerKey);
            if (deletedGoalAnswer == null)
            {
                return NotFound();
            }

            var mappedDeletedGoalAnswer = _mapper.Map<GoalAnswer>(deletedGoalAnswer);
            return Ok(mappedDeletedGoalAnswer);
        }
        
        [HttpGet("[action]/{goalKey}/{date}")]
        public IActionResult GoalAnswersTotal(long goalKey, DateTime date)
        {
            var goalAnswerTotals = _goalAnswerService.GoalAnswersTotal(goalKey, date);
            return Ok(_mapper.Map<GoalAnswerTotal>(goalAnswerTotals));
        }
    }
}