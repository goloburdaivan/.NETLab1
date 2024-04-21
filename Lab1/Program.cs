using Lab1;
using Lab1.Factory;
using Lab1.Generators;
using Lab1.Models;
using Lab1.Queries;
using System.Reflection;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = Startup.ConfigureServices();
            ScheduleQueries? queries = serviceProvider.GetService(typeof(ScheduleQueries)) as ScheduleQueries;

            MethodInfo[] methods = typeof(ScheduleQueries).GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methods)
            {
                if (method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
                {
                    Console.WriteLine($"----- {method.Name} -----");
                    method.Invoke(queries, null);
                }
            }
        }
    }
}