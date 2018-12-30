using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.Server.Controllers
{
    public class GoalAnswerController : Controller
    {
        private readonly IGoalAnswerService _goalAnswerService;
        private readonly IMapper _mapper;

        public GoalAvailableAnswerController(IGoalAnswerService goalAnswerService, IMapper mapper)
        {
            _goalAnswerService = goalAnswerService;
            _mapper = mapper;
        }
        
        [HttpGet("[action]/{goalKey}")]
        public async Task<IActionResult> DeleteAllForGoal(long goalKey)
        {
            
        }
    }
}