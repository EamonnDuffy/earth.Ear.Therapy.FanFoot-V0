using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using earth.Ear.Therapy.FanFoot.External;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace earth.Ear.Therapy.FanFoot
{
    public class Startup
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureLogging()
        {
            try
            {
                var entryAssembly = Assembly.GetEntryAssembly();

                var assemblyPath = Path.GetDirectoryName(entryAssembly.Location);

                var logRepository = LogManager.GetRepository(entryAssembly);

                using (var fileStream = File.Open(Path.Combine(assemblyPath, "Log4Net.config"), FileMode.Open))
                {
                    var collection = XmlConfigurator.Configure(logRepository, fileStream);
                }

                Log.Info("The Application is starting...");
            }
            catch (Exception exception)
            {
                int breakPoint = 1;
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogging();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });

            services.AddDbContext<FanFootTherapyDatabase>(options =>
                options.UseSqlite(Configuration.GetConnectionString("FanFootTherapyDatabase")));

            services.AddScoped<IFanFootTherapyDatabase, FanFootTherapyDatabase>();

            services.AddTransient<IDatabaseVersionsRepository, DatabaseVersionsRepository>();
            services.AddTransient<ISeasonsRepository, SeasonsRepository>();
            services.AddTransient<ITeamsRepository, TeamsRepository>();
            services.AddTransient<IPlayersRepository, PlayersRepository>();

            services.AddTransient<ITeamsWeeklyResults, TeamsWeeklyResults>();
            services.AddTransient<ITeamsWeeklyDocuments, TeamsWeeklyDocuments>();

            services.AddRazorPages()
                //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo("en-IE");
            //cultureInfo.NumberFormat.CurrencySymbol = "€";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
