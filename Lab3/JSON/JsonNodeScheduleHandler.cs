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
                        ClassroomSubject = ParseClassroomSubject(node!["ClassroomSubject"]),
                        Date = node!["Date"].GetValue<DateTime>(),
                        Groups = JsonSerializer.Deserialize<List<Group>>(node!["Groups"]),
                        Teacher = ParseTeacher(node!["Teacher"])
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

        private ClassroomSubject ParseClassroomSubject(JsonNode node)
        {
            ClassroomSubject result = new ClassroomSubject
            {
                ClassroomId = node!["ClassroomId"].GetValue<int>(),
                SubjectId = node!["SubjectId"].GetValue<int>(),
                Subject = ParseSubject(node!["Subject"]),
                Classroom = ParseClassroom(node!["Classroom"])
            };

            return result;
        }

        private Subject ParseSubject(JsonNode node)
        {
            Subject result = new Subject
            {
                Id = node!["Id"].GetValue<int>(),
                Name = node!["Name"].GetValue<string>(),
                ClassroomId = node!["ClassroomId"].GetValue<int>()
            };

            return result;
        }

        private Classroom ParseClassroom(JsonNode node)
        {
            Classroom result = new Classroom
            {
                Id = node!["Id"].GetValue<int>(),
                RoomNumber = node!["RoomNumber"].GetValue<int>(),
                SubjectId = node!["SubjectId"].GetValue<int>()
            };

            return result;
        }

        private Teacher ParseTeacher(JsonNode node)
        {
            Teacher teacher = new Teacher
            {
                CathedraId = node!["CathedraId"].GetValue<int>(),
                Id = node!["Id"].GetValue<int>(),
                Name = node!["Name"].GetValue<string>(),
                Surname = node!["Surname"].GetValue<string>(),
                Cathedra = PraseCathedra(node!["Cathedra"])
            };

            return teacher;
        }

        private Faculcy ParseFaculcy(JsonNode node)
        {
            Faculcy result = new Faculcy
            {
                Id = node!["Id"].GetValue<int>(),
                Title = node!["Title"].GetValue<string>()
            };

            return result;
        }

        private Cathedra PraseCathedra(JsonNode node)
        {
            Cathedra cathedra = new Cathedra
            {
                Faculcy = ParseFaculcy(node["Faculcy"]),
                FaculcyId = node!["FaculcyId"].GetValue<int>(),
                Title = node!["Title"].GetValue<string>(),
                Id = node!["Id"].GetValue<int>()
            };

            return cathedra;
        } 
    }
}
