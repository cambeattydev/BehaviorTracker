using BehaviorTracker.Repository.Implementations;
using BehaviorTracker.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BehaviorTracker.Repository
{
    public static class IocConfiguration
    {
        public static void ConfigureIoc(IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>()
                .AddScoped<IGoalRepository, GoalRepository>()
                .AddScoped<IGoalAvailableAnswerRepository, GoalAvailableAnswerRepository>()
                .AddScoped<IGoalAnswerRepository, GoalAnswerRepository>();
        }
    }
}