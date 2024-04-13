using Lab1.Models;
using Lab1.Utility;
using System.Xml;

namespace Lab1.Services
{
    public class XMLCreator : IDisposable
    {

        private readonly XmlWriter _writer;
        public string FileName { get; private set; }
        public XMLCreator(string fileName) {
            FileName = fileName;
            XmlWriterSettings settings = new XmlWriterSettings();
            _writer = XmlWriter.Create(fileName, settings);
        }
        public XMLCreator() : this("schedule.xml") { }


        private void GenerateXmlStructure(ScheduleItem item)
        {
            _writer.WriteStartElement("item");
            _writer.WriteAttributeString("id", item.Id.ToString());

            _writer.WriteStartElement(ScheduleXMLStructure.ClassroomElement);
            _writer.WriteElementString("room", item.ClassroomSubject.Classroom.RoomNumber.ToString());
            _writer.WriteElementString("subjectId", item.ClassroomSubject.Classroom.SubjectId.ToString());
            _writer.WriteEndElement();

            _writer.WriteStartElement(ScheduleXMLStructure.SubjectElement);
            _writer.WriteElementString(ScheduleXMLStructure.TitleElement, item.ClassroomSubject.Subject.Name);
            _writer.WriteElementString("classroomId", item.ClassroomSubject.Subject.ClassroomId.ToString());
            _writer.WriteEndElement();

            _writer.WriteStartElement(ScheduleXMLStructure.TeacherElement);
            _writer.WriteAttributeString("id", item.Teacher.Id.ToString());
            _writer.WriteElementString("name", item.Teacher.Name);
            _writer.WriteElementString("surname", item.Teacher.Surname);
            _writer.WriteStartElement(ScheduleXMLStructure.CathedraElement);
            _writer.WriteAttributeString("id", item.Teacher.Cathedra.Id.ToString());
            _writer.WriteElementString("facultyId", item.Teacher.Cathedra.FaculcyId.ToString());
            _writer.WriteElementString(ScheduleXMLStructure.TitleElement, item.Teacher.Cathedra.Title);
            _writer.WriteEndElement();

            _writer.WriteStartElement(ScheduleXMLStructure.FacultyElement);
            _writer.WriteAttributeString("id", item.Teacher.Cathedra.Faculcy.Id.ToString());
            _writer.WriteElementString(ScheduleXMLStructure.TitleElement, item.Teacher.Cathedra.Faculcy.Title);
            _writer.WriteEndElement();

            _writer.WriteEndElement();

            _writer.WriteStartElement(ScheduleXMLStructure.GroupsElement);
            foreach (var group in item.Groups)
            {
                _writer.WriteStartElement("group");
                _writer.WriteAttributeString("id", group.Id.ToString());
                _writer.WriteElementString("code", group.Code);
                _writer.WriteEndElement();
            }

            _writer.WriteEndElement();
            _writer.WriteElementString(ScheduleXMLStructure.DateElement, item.Date.ToString());

            _writer.WriteEndElement();
        }

        public void CreateScheduleFileSerialized(IEnumerable<ScheduleItem> schedule)
        {
            Serializer.SerializeToXml(schedule.ToArray(), FileName);
        }

        public void CreateScheduleFile(IEnumerable<ScheduleItem> schedule)
        {
            _writer.WriteStartElement(ScheduleXMLStructure.RootElement);
            foreach (var item in schedule)
            {
                GenerateXmlStructure(item);
            }

            _writer.WriteEndElement();
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose called");
            _writer.Close();
            _writer.Dispose();
        }
    }
}
