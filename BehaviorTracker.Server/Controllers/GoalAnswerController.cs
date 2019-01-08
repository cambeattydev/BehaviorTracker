using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
                return NotFound();
            }

            return Ok(studentGoalAnswers.ToDictionary(goalAnswer => goalAnswer.Key,
                kvp => _mapper.Map<Client.Models.GoalAnswer>(kvp.Value)));

        }
        
        
        [HttpPost("[action]")]
        public async Task<IActionResult> GoalAnswer([FromBody] Client.Models.GoalAnswer goalAnswer)
        {
            var mappedGoalAnswer = _mapper.Map<Service.Models.GoalAnswer>(goalAnswer);
            var savedGoalAnswer = await _goalAnswerService.SaveAsync(mappedGoalAnswer);
            
            return Ok(_mapper.Map<Client.Models.GoalAnswer>(savedGoalAnswer));

        }
    }
}