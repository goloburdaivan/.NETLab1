using Lab1.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Lab1.JSON
{
    public class JsonNodeScheduleHandler : IJsonScheduleHandler
    {
        public List<ScheduleItem> DeserializeFile(Stream file)
        {
            List<ScheduleItem> result = new List<ScheduleItem>();
            using (var reader = new StreamReader(file))
            {
                string json = reader.ReadToEnd();
                JsonNode nodes = JsonNode.Parse(json);
                foreach (var node in nodes.AsArray()) {
                    ScheduleItem item = new ScheduleItem
                    {
                        Id = node!["Id"].GetValue<int>(),
                        ClassroomSubject = JsonSerializer.Deserialize<ClassroomSubject>(node!["ClassroomSubject"]),
                        Date = node!["Date"].GetValue<DateTime>(),
                        Groups = JsonSerializer.Deserialize<List<Group>>(node!["Groups"]),
                        Teacher = JsonSerializer.Deserialize<Teacher>(node!["Teacher"])
                    };
                    result.Add(item);
                }
            }
            return result;
        }

        public void SerializeFile(List<ScheduleItem> obj, Stream file)
        {
            var rootNode = new JsonArray();

            foreach (var scheduleItem in obj)
            {
                var scheduleItemNode = new JsonObject
                {
                    ["Id"] = scheduleItem.Id,
                    ["Teacher"] = JsonNode.Parse(JsonSerializer.Serialize(scheduleItem.Teacher)),
                    ["Groups"] = JsonNode.Parse(JsonSerializer.Serialize(scheduleItem.Groups)),
                    ["ClassroomSubject"] = JsonNode.Parse(JsonSerializer.Serialize(scheduleItem.ClassroomSubject)),
                    ["Date"] = JsonNode.Parse(JsonSerializer.Serialize(scheduleItem.Date))
                };
                rootNode.Add(scheduleItemNode);
            }

            using (var writer = new StreamWriter(file))
            {
                writer.Write(rootNode.ToString());
            }
        }
    }
}
