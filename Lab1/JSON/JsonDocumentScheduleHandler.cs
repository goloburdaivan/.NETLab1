using Lab1.Models;
using System.Text.Json;

namespace Lab1.JSON
{
    public class JsonDocumentScheduleHandler : IJsonScheduleHandler
    {
        public List<ScheduleItem> DeserializeFile(Stream file)
        {
            using (var streamReader = new StreamReader(file))
            {
                using (var jsonDocument = JsonDocument.Parse(streamReader.ReadToEnd()))
                {
                    var scheduleItems = new List<ScheduleItem>();
                    foreach (var element in jsonDocument.RootElement.EnumerateArray())
                    {
                        var scheduleItem = new ScheduleItem
                        {
                            Id = element.GetProperty("Id").GetInt32(),
                            Teacher = element.GetProperty("Teacher").Deserialize<Teacher>(),
                            ClassroomSubject = element.GetProperty("ClassroomSubject").Deserialize<ClassroomSubject>(),
                            Groups = DeserializeGroups(element.GetProperty("Groups").GetRawText()),
                            Date = element.GetProperty("Date").GetDateTime()
                        };
                        scheduleItems.Add(scheduleItem);
                    }
                    return scheduleItems;
                }
            }
        }

        private List<Group> DeserializeGroups(string json)
        {
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                List<Group> list = new List<Group>();
                JsonElement root = document.RootElement;

                foreach (JsonElement element in root.EnumerateArray())
                {
                    list.Add(JsonSerializer.Deserialize<Group>(element.GetRawText()));
                }

                return list;
            }
        }

        public void SerializeFile(List<ScheduleItem> obj, Stream file)
        {
            using (var streamWriter = new StreamWriter(file))
            {
                using (var jsonWriter = new Utf8JsonWriter(streamWriter.BaseStream, new JsonWriterOptions { Indented = true }))
                {
                    jsonWriter.WriteStartArray();

                    foreach (var scheduleItem in obj)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WriteNumber("Id", scheduleItem.Id);
                        jsonWriter.WritePropertyName("Teacher");
                        JsonSerializer.Serialize(jsonWriter, scheduleItem.Teacher);
                        jsonWriter.WritePropertyName("ClassroomSubject");
                        JsonSerializer.Serialize(jsonWriter, scheduleItem.ClassroomSubject);

                        jsonWriter.WritePropertyName("Groups");
                        jsonWriter.WriteStartArray();
                        foreach (var group in scheduleItem.Groups)
                        {
                            JsonSerializer.Serialize(jsonWriter, group);
                        }
                        jsonWriter.WriteEndArray();

                        jsonWriter.WriteString("Date", scheduleItem.Date.ToString("yyyy-MM-ddTHH:mm:ss"));
                        jsonWriter.WriteEndObject();
                    }

                    jsonWriter.WriteEndArray();
                }
            }
        }
    }
}
