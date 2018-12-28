using System;
using BehaviorTracker.Client.Models;
using BehaviorTracker.Client.Shared.Common;
using BehaviorTracker.Client.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BehaviorTracker.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IValidator<Student>, StudentValidator>();
            services.AddTransient<IValidator<Goal>, GoalValidator>();
            services.AddTransient<IValidator<GoalAvailableAnswerEditModel>, GoalAvailableAnswerEditModelValidator>();
            services.AddSingleton<IValidatorFactory, ValidatorFactory>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
           
        }
    }
}