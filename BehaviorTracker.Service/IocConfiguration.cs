using BehaviorTracker.Repository.Implementations;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Implementations;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BehaviorTracker.Service
{
    public static class IocConfiguration
    {
        public static void ConfigureIoc(IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>()
                .AddScoped<IGoalService, GoalService>()
                .AddScoped<IGoalAvailableAnswerService, GoalAvailableAnswerService>()
                .AddScoped<IGoalAnswerService, GoalAnswerService>()
                .AddScoped<IUserService, UserService>();
            
            Repository.IocConfiguration.ConfigureIoc(services);
        }
    }
}