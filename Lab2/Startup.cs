using Lab1.Generators;
using Lab1.Models;
using Lab1.Queries;
using Lab1.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Xml;

namespace Lab1
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddTransient<FaculcyGenerator>()
                .AddTransient<Random>()
                .AddTransient<Schedule>()
                .AddTransient<ScheduleGenerator>()
                .AddTransient<ScheduleQueries>()
                .AddSingleton<XMLCreator>()
                .AddSingleton<XMLPrinter>()
                .AddSingleton<ScheduleQueriesXml>()
                .BuildServiceProvider();
        }
    }
}
