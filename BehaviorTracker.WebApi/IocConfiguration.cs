using BehaviorTracker.Repository.Implementations;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Implementations;
using BehaviorTracker.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BehaviorTracker.WebApi
{
    public static class IocConfiguration
    {
        public static void ConfigureIoc(IServiceCollection services)
        {
         Service.IocConfiguration.ConfigureIoc(services);
        }
    }
}