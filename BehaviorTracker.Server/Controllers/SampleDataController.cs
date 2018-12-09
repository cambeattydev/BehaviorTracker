using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorTracker.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static readonly StudentAndGoals[] StudentsAndGoals =
        {
            new StudentAndGoals("Student 1", new[]
            {
                new GoalResponse
                {
                    GoalType = GoalType.YesNo,
                    Name = "I can speak respectfully."
                },
                new GoalResponse
                {
                    GoalType = GoalType.YesNo,
                    Name = "I can act appropriately."
                }, 
            }),
            new StudentAndGoals("Student 2", new[]
            {
                new GoalResponse
                {
                    GoalType = GoalType.Numeric,
                    Name = "I have a number type answer."
                }
                 
            }),
            new StudentAndGoals("Student 3", new[]
            {
                new GoalResponse
                {
                    GoalType = GoalType.YesNo,
                    Name = "I have a yes/no type answer."
                }
                 
            }),
        };

        [HttpGet("[action]")]
        public IEnumerable<StudentAndGoals> StudentAndGoals() => StudentsAndGoals;
    }
}