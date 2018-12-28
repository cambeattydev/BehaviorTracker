using BehaviorTracker.Repository.Implementations;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Implementations;
using BehaviorTracker.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BehaviorTracker.Service
{
    public static class IocConfiguration
    {
        public static void ConfigureIoc(IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>()
                .AddScoped<IGoalService, GoalService>()
                .AddScoped<IGoalAvailableAnswerService, GoalAvailableAnswerService>();
            
            Repository.IocConfiguration.ConfigureIoc(services);
        }
    }
}