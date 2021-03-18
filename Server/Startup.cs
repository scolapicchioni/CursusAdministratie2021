using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using CursusAdministratie2021.Shared.Interfaces;
using CursusAdministratie2021.Shared.Services;
using CursusAdministratie2021.Server.Infrastructure.Repositories;
using CursusAdministratie2021.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Server.Core.Services;
using CursusAdministratie2021.Shared.CalendarHelpers;

namespace CursusAdministratie2021.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoursesAdministrationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CoursesAdministrationDbContext")));

            services.AddScoped<ICoursesOverviewRepository, CoursesOverviewRepositoryEF>();
            services.AddScoped<ICoursesOverviewService, CoursesOverviewService>();

            services.AddScoped<ICoursesImporterRepository, CoursesImporterRepositoryEF>();
            services.AddScoped<ICoursesImporterService, CoursesImporterService>();

            services.AddScoped<ICalendarHelper, CalendarHelper>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
