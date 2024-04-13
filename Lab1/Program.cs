using Lab1;
using Lab1.Factory;
using Lab1.Generators;
using Lab1.Models;
namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var serviceProvider = Startup.ConfigureServices();
            ScheduleGenerator? generator = serviceProvider.GetService(typeof(ScheduleGenerator)) as ScheduleGenerator;

            if (generator == null)
            {
                Console.WriteLine("Error getting depedency from container");
                return;
            }

            Schedule schedule = generator.GenerateSchedule(100);
            List<ScheduleItem> scheduleItems = schedule.ToList();
            using (var stream = File.Create("schedule.json"))
            {
                var handler = JsonHandlerFactory.CreateJsonHandler("JsonNode");
                handler.SerializeFile(scheduleItems, stream);
            }

            using (var stream = File.Open("schedule.json", FileMode.Open))
            {
                var handler = JsonHandlerFactory.CreateJsonHandler("JsonDocument");
                var items = handler.DeserializeFile(stream);
                foreach (var item in items)
                {
                    item.Print();
                }
            }

        }
    }
}