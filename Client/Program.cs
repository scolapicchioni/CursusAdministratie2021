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
using CursusAdministratie2021.Client.Core.CourseParsers;
using CursusAdministratie2021.Client.Core.CourseParsers.CoursePropertyParsers;
using CursusAdministratie2021.Client.Core.Interfaces;
using CursusAdministratie2021.Client.Core.Services;
using CursusAdministratie2021.Shared.CalendarHelpers;
using FluentValidation;
using CursusAdministratie2021.Shared.Models;
using CursusAdministratie2021.Shared.Validators;

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

            builder.Services.AddTransient<ICourseParser>(sp => new CourseParser(new CourseTitleParser(), new CourseCodeParser(), new CourseDurationParser(), new CourseStartDateParser(), new EmptyLineParser()));

            builder.Services.AddScoped<ICoursesImporterService, CoursesImporterService>();
            builder.Services.AddScoped<ICoursesImporterRepository, CoursesImporterRepositoryRestClient>();

            builder.Services.AddScoped<IStudentsService, StudentsService>();
            builder.Services.AddScoped<IStudentsRepository, StudentsRepositoryRestClient>();

            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepositoryRestClient>();


            builder.Services.AddScoped<ICalendarHelper, CalendarHelper>();

            builder.Services.AddTransient<IValidator<PrivateCitizen>, PrivateCitizenValidator>();
            builder.Services.AddTransient<IValidator<CompanyEmployee>, CompanyEmployeeValidator>();

            await builder.Build().RunAsync();
        }
    }
}
