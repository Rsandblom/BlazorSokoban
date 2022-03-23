using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sokoban.API.Models;
using Sokoban.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokoban.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    Policies.Policies.CanCreateLevels,
                    Policies.Policies.CanCreateLevelsPolicy());
                authorizationOptions.AddPolicy(
                    Policies.Policies.CanPlayLevels,
                    Policies.Policies.CanPlayLevelsPolicy());
            });

            var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();

            // requires using Microsoft.Extensions.Options
            services.Configure<LevelstoreDatabaseSettings>(
                Configuration.GetSection(nameof(LevelstoreDatabaseSettings)));

            services.AddSingleton<ILevelstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<LevelstoreDatabaseSettings>>().Value);

            services.AddAuthentication(
               IdentityServerAuthenticationDefaults.AuthenticationScheme)
           .AddIdentityServerAuthentication(options =>
           {
               options.Authority = "https://localhost:44350/";
               options.ApiName = "sokobanapi";
           });

            services.AddScoped<ILevelRepository, LevelRepository>();

            services.AddControllers(configure =>
            configure.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy)));

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("Open");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
