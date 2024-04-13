using Lab1.Comparators;
using Lab1.Generators;
using Lab1.Models;
using Lab1.Services;
using System.Xml.Linq;

namespace Lab1.Queries
{
    public class ScheduleQueriesXml
    {
        private readonly ScheduleGenerator _generator;
        private readonly XMLCreator _xmlCreator;
        private readonly XDocument _xmlDocument;
        private IEnumerable<Cathedra> _cathedras;
        private IEnumerable<Faculcy> _faculcies;
        public int GenerationSize { get; set; } = 100;

        public ScheduleQueriesXml(ScheduleGenerator generator, XMLCreator xmlCreator)
        {
            _generator = generator;
            _xmlCreator = xmlCreator;
            _xmlCreator.CreateScheduleFile(_generator.GenerateSchedule(GenerationSize));
            _xmlCreator.Dispose();

            _xmlDocument = XDocument.Load("schedule.xml");
        }

        public void GetCathedrasSorted()
        {
            var cathedras = _xmlDocument.Descendants("cathedra").Select(item => new Cathedra
            {
                Id = int.Parse(item.Attribute("id").Value),
                Title = item.Element("title").Value,
                FaculcyId = int.Parse(item.Element("facultyId").Value)
            })
            .DistinctBy(item => item.Id).OrderBy(item => item.Title);

            _cathedras = cathedras.AsEnumerable();


            foreach (var item in _xmlDocument.Descendants("cathedra").Select(c => c.Element("title").Value).Distinct().OrderBy(e => e))
            {
                Console.WriteLine(item);
            }
        }
        
        public void GetFacultySortedDesc()
        {
            var faculcies = _xmlDocument.Descendants("faculty").Select(item => new Faculcy
            {
                Id = int.Parse(item.Attribute("id").Value),
                Title = item.Element("title").Value
            })
            .DistinctBy(item => item.Id).OrderBy(item => item.Title);

            _faculcies = faculcies.AsEnumerable();

            foreach (var item in _xmlDocument.Descendants("faculty").Select(c => c.Element("title").Value).Distinct().OrderByDescending(e => e))
            {
                Console.WriteLine(item);
            }
        }

        public void GetTeachersSubjectsWithClassrooms()
        {
            foreach (var item in _xmlDocument.Descendants("item")
                .Where(item => item.Element("teacher").Attribute("id").Value == "30")
                .Select(item => new
                {
                    Subject = item.Element("subject").Element("title").Value,
                    Classroom = item.Element("classroom").Element("room").Value
                }))
            {
                Console.WriteLine($"{item.Subject} => {item.Classroom}");
            }
        }

        public void GetSubjectsForEachTeacher()
        {
            var subjects = _xmlDocument.Descendants("item")
                .GroupBy(
                    item => new
                    {
                        TeacherName = item.Element("teacher").Element("name").Value,
                        TeacherSurname = item.Element("teacher").Element("surname").Value
                    },
                    (key, items) => new
                    {
                        Teacher = new
                        {
                            Name = key.TeacherName,
                            Surname = key.TeacherSurname
                        },
                        Subjects = items.Select(item => item.Element("classroom").Element("subjectId").Value).ToList()
                    });

            foreach (var subjectGroup in subjects)
            {
                Console.WriteLine($"Teacher: {subjectGroup.Teacher.Name} {subjectGroup.Teacher.Surname}");
                foreach (var subject in subjectGroup.Subjects)
                {
                    Console.WriteLine($"Subject: {subject}");
                }
                Console.WriteLine();
            }
        }

        public void GetTeacherWithMaxSubjects()
        {
            var teacher = _xmlDocument.Descendants("item")
                .GroupBy(item => item.Element("teacher").Attribute("id").Value)
                .OrderByDescending(group => group.Count())
                .FirstOrDefault();

            if (teacher != null)
            {
                Console.WriteLine($"{_xmlDocument.Descendants("teacher")
                    .Where(item => item.Attribute("id").Value == teacher.Key)
                    .First()
                    .Element("name").Value} " +
                    $"{teacher.Count()}");
            }
            else
            {
                throw new ArgumentException("Exception during generation");
            }
        }

        public void GetTeachersOrderByAlphabet()
        {
            var teachers = _xmlDocument.Descendants("teacher").Select(item => item.Element("name").Value).Select(item => new
            {
                Teacher = item
            }).Distinct().OrderBy(item => item.Teacher);

            foreach (var item in teachers)
            {
                Console.WriteLine(item.Teacher);
            }
        }

        public void GetScheduleItemsCount()
        {
            int count = _xmlDocument.Descendants("item").Where(item => DateTime.Parse(item.Element("date").Value) >= DateTime.UtcNow.AddDays(25)).Count();
            Console.WriteLine(count);
        }

        public void GetCathedrasOnEachFaculcy()
        {
            var cathedrasWithFaculties =
                from cathedra in _cathedras
                join faculty in _faculcies on cathedra.FaculcyId equals faculty.Id
                select new { Cathedra = cathedra, Faculty = faculty };

            foreach (var item in cathedrasWithFaculties)
            {
                Console.WriteLine($"{item.Cathedra.Title} - {item.Faculty.Title}");
            }
        }

        public void UnionSchedules()
        {
            var unionSchedule = _xmlDocument.Descendants("item").Take(10)
                .UnionBy(_xmlDocument.Descendants("item").Skip(20).Take(20), x => x.Attribute("id").Value)
                .Select(item => new
                {
                    Subject = item.Element("subject").Element("title").Value,
                    Date = item.Element("date").Value
                });
            foreach (var item in unionSchedule)
            {
                Console.WriteLine($"{item.Subject} {item.Date}");
            }

            Console.WriteLine($"Total count: {unionSchedule.Count()}");
        }
        public void ExceptItemsSchedule()
        {
            var exceptSchedule = _xmlDocument.Descendants("item").Take(20)
                .Except(_xmlDocument.Descendants("item").Take(10))
                .Select(item => new
                {
                    Subject = item.Element("subject").Element("title").Value,
                    Date = item.Element("date").Value
                });
            foreach (var item in exceptSchedule)
            {
                Console.WriteLine($"{item.Subject} {item.Date}");
            }

            Console.WriteLine($"Total count: {exceptSchedule.Count()}");
        }

        public void SumDigitsInName()
        {
            var sumDigits = (from item in _xmlDocument.Descendants("teacher")
                             let digitsSum = item.Element("name").Value
                                 .Where(char.IsDigit)
                                 .Select(c => int.Parse(c.ToString()))
                                 .Sum()
                             select new
                             {
                                 Teacher = item.Element("name").Value,
                                 DigitsSum = digitsSum
                             }).Take(10);

            foreach (var item in sumDigits)
            {
                Console.WriteLine($"Teacher {item.Teacher} - {item.DigitsSum}");
            }
        }

        public void FindTeachersWith7InTheEnd()
        {
            Console.WriteLine($"Teachers with '7' in the end: {_xmlDocument
                .Descendants("teacher")
                .Where(item => item.Element("name").Value.EndsWith("7"))
                .Count()}");
        }

        public void GetMaxClassroomNumber()
        {
            var maxNumber = (from item in _xmlDocument.Descendants("classroom")
                             select int.Parse(item.Element("room").Value))
                    .Aggregate((acc, x) => acc > x ? acc : x);
            Console.WriteLine(maxNumber);
        }

        public void IsAnyRoomLessThan100()
        {
            bool result = _xmlDocument.Descendants("classroom").Any(item => int.Parse(item.Element("room").Value) < 100);
            Console.WriteLine(result);
        }

        public void GetLookUpByClassrooms()
        {
            foreach (var item in _xmlDocument.Descendants("classroom")
                .ToLookup(item => int.Parse(item.Element("room").Value)).Take(5))
            {
                Console.WriteLine($"{item.Key} - {item.Count()}");
            }
        }

        public void CreateSerialized()
        {
            _xmlCreator.CreateScheduleFileSerialized(_generator.GenerateSchedule(100));
        }
    }
}
