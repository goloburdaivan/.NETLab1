using Lab1;
using Lab1.Queries;
using Lab1.Services;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = Startup.ConfigureServices();
        ScheduleQueriesXml? queries = serviceProvider.GetService(typeof(ScheduleQueriesXml)) as ScheduleQueriesXml;

        MethodInfo[] methods = typeof(ScheduleQueriesXml).GetMethods(BindingFlags.Public | BindingFlags.Instance);

        foreach (var method in methods)
        {
            if (method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
            {
                Console.WriteLine($"----- {method.Name} -----");
                method.Invoke(queries, null);
            }
        }

        XMLPrinter printer = new XMLPrinter();
        printer.PrintSerialized();
    }
}