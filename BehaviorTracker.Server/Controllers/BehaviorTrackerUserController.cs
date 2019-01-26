using AutoMapper;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using BehaviorTracker.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.Server.Controllers
{
    public class BehaviorTrackerUserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BehaviorTrackerUserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        
        [HttpGet("[action]")]
        [Authorize(Roles = nameof(BehaviorTrackerRoles.UserRead))]
        public IActionResult Users()
        {
            var users = _userService.GetUsers();
            _mapper.Map<BehaviorTrackerUser>()
        }
    }
}