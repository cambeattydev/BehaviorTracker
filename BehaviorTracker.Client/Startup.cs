using BehaviorTracker.Client.Shared.Common;
using BehaviorTracker.Client.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BehaviorTracker.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IValidatorFactory,ValidationFactory>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
           
        }
    }
}