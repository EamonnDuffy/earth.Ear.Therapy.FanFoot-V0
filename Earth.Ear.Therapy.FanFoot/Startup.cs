using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Earth.Ear.Therapy.FanFoot
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FanFootTherapyDatabase>(options =>
                options.UseSqlite(Configuration.GetConnectionString("FanFootTherapyDatabase")));

            services.AddScoped<IFanFootTherapyDatabase, FanFootTherapyDatabase>();

            services.AddTransient<IDatabaseVersionsRepository, DatabaseVersionsRepository>();
            services.AddTransient<ISeasonsRepository, SeasonsRepository>();
            services.AddTransient<ITeamsRepository, TeamsRepository>();
            services.AddTransient<IPlayersRepository, PlayersRepository>();

            services.AddMvc()
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting(routes =>
            {
                routes.MapControllerRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRazorPages();
            });

            app.UseCookiePolicy();

            app.UseAuthorization();
        }
    }
}