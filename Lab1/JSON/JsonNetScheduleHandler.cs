using Lab1.Models;
using System.Text.Json;

namespace Lab1.JSON
{
    public class JsonNetScheduleHandler : IJsonScheduleHandler
    {
        public List<ScheduleItem> DeserializeFile(Stream file)
        {
            using (var reader = new StreamReader(file))
            {
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<List<ScheduleItem>>(json);
            }
        }

        public void SerializeFile(List<ScheduleItem> obj, Stream file)
        {
            using (var writer = new Utf8JsonWriter(file, new JsonWriterOptions { Indented = true }))
            {
                JsonSerializer.Serialize(writer, obj);
            }
        }
    }
}
