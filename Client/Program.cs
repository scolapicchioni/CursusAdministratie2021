using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CursusAdministratie2021.Shared.Services;
using CursusAdministratie2021.Shared.Interfaces;
using CursusAdministratie2021.Client.Infrastructure.Repositories;

namespace CursusAdministratie2021.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ICoursesOverviewService,CoursesOverviewService>();
            builder.Services.AddScoped<ICoursesOverviewRepository, CoursesOverviewRepositoryRestClient>();

            await builder.Build().RunAsync();
        }
    }
}
