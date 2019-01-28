using AutoMapper;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class BehaviorTrackerUserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BehaviorTrackerUserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "AddAdmin, AddGoalAnswerWriter")]
        public IActionResult Users()
        {
            var users = _userService.GetUsers();
            var mappedUsers = _mapper.Map<BehaviorTrackerUser>(users);
            return Ok(mappedUsers);
        }
    }
}