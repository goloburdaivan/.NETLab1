using Lab1.Generators;
using Lab1.Models;
using Lab1.Utility;
using Microsoft.Extensions.DependencyInjection;

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
                .BuildServiceProvider();
        }
    }
}
