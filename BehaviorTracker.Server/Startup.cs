using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using AutoMapper;
using BehaviorTracker.Repository.Implementations;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using BehaviorTracker.Server.Auth;
using BehaviorTracker.Server.Mappings;
using BehaviorTracker.Service.Implementations;
using BehaviorTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BehaviorTracker.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Repository.BehaviorTrackerDatabaseContext>(options =>
                options.UseSqlite(Repository.BehaviorTrackerDatabaseContext.ConnectionString));

            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
                
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            AddAutoMapper(services);

//            services.AddIdentity<BehaviorTrackerUser, BehaviorTrackerRole>()
//                .AddEntityFrameworkStores<Repository.BehaviorTrackerDatabaseContext>()
//                .AddDefaultTokenProviders();

//            services.Configure<IdentityOptions>(options =>
//            {
//                options.Password.RequireDigit = false;
//                options.Password.RequiredLength = 1;
//                options.Password.RequireLowercase = false;
//                options.Password.RequireUppercase = false;
//                options.Password.RequiredUniqueChars = 1;
//                options.Password.RequireNonAlphanumeric = false;
//                
//                options.Lockout.AllowedForNewUsers = false;
//                options.Lockout.MaxFailedAccessAttempts = int.MaxValue;
//                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
//                
//                options.SignIn.RequireConfirmedEmail = false;
//                options.SignIn.RequireConfirmedPhoneNumber = false;
//            });

            services.AddAuthentication(authenticationOptions =>
                {
                    authenticationOptions.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = _configuration["GoogleAuthentication:client_id"];
                    googleOptions.ClientSecret = _configuration["GoogleAuthentication:client_secret"];
                    googleOptions.Events = new GoogleAuthEvents("cbcsd.org");
                })
                .AddCookie();

            services.ConfigureApplicationCookie(options =>
            {
                options.SlidingExpiration = true;
            });

            IocConfiguration.ConfigureIoc(services);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Repository.BehaviorTrackerDatabaseContext>();
#if DEBUG
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
#else
                context.Database.Migrate();
#endif
            }

            app.UseAuthentication();
            app.UseCookiePolicy();
            
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}"); });

            app.UseBlazor<BehaviorTracker.Client.Startup>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ServerToServiceProfile>();
                cfg.AddProfile<Service.Mapping.ServiceToRepositoryProfile>();
            });

            services.AddAutoMapper();
        }
    }
}