using Lab1.Interfaces;
using Lab1.Models;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab1.Services
{
    public class XMLPrinter : IPrintable
    {

        public string FileName { get; private set; }
        private readonly XDocument _document;
        public XMLPrinter(string fileName)
        {
            FileName = fileName;
            _document = XDocument.Load(FileName);
        }

        public XMLPrinter() : this("schedule.xml") { }

        public void Print()
        {
            foreach (var scheduleItem in _document.Element("schedule").Elements("item"))
            {
                var classRoom = scheduleItem.Element("classroom");
                var subject = scheduleItem.Element("subject");
                var teacher = scheduleItem.Element("teacher");
                var cathedra = teacher.Element("cathedra");
                var faculty = teacher.Element("faculty");
                var date = scheduleItem.Element("date");

                Console.WriteLine($"Classroom {classRoom.Element("room").Value}");
                Console.WriteLine($"Subject {subject.Element("title").Value}");
                Console.WriteLine($"Teacher: {teacher.Element("name").Value} {teacher.Element("surname").Value}");
                Console.WriteLine($"Cathedra: {cathedra.Element("title").Value}");
                Console.WriteLine($"Faculty: {faculty.Element("title").Value}");
                Console.WriteLine($"Date: {date.Value}");
                Console.WriteLine("-----------");
            }
        }

        public void PrintSerialized()
        {
            var serializer = new XmlSerializer(typeof(ScheduleItem[]));
            using (var reader = XmlReader.Create("schedule.xml"))
            {
                var scheduleItems = (ScheduleItem[])serializer.Deserialize(reader);
                foreach (var scheduleItem in scheduleItems)
                {
                    scheduleItem.Print();
                }
            }
        }
    }
}
