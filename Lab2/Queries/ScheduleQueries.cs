using Lab1.Comparators;
using Lab1.Generators;
using Lab1.Models;

namespace Lab1.Queries
{
    public class ScheduleQueries
    {
        private readonly ScheduleGenerator _generator;
        private readonly ScheduleGenerator _copyGenerator;
        private readonly Schedule _schedule;
        private readonly Schedule _copySchedule;
        private readonly IEnumerable<Cathedra> _cathedras;
        private readonly IEnumerable<Faculcy> _faculcies;

        public ScheduleQueries(ScheduleGenerator generator, ScheduleGenerator copyGenerator)
        {
            _generator = generator;
            _copyGenerator = copyGenerator;
            _schedule = _generator.GenerateSchedule(100);
            _copySchedule = _copyGenerator.GenerateSchedule(10);

            _cathedras = (from item in _schedule
                          select item.Teacher into t
                          select t.Cathedra).Distinct();
            _faculcies = (from item in _cathedras
                          select item.Faculcy).Distinct();

            if (generator == null || copyGenerator == null)
            {
                throw new ArgumentNullException("Error injecting ScheduleGenerator");
            }
        }

        public void GetCathedras()
        {
            foreach (var item in _cathedras)
            {
                Console.WriteLine(item.Title);
            }
        }

        public void GetFaculcies()
        {
            foreach (var item in _faculcies)
            {
                Console.WriteLine(item.Title);
            }
        }

        public void GetAllRecords()
        {
            foreach (var item in _schedule.Select(item => item))
            {
                item.Print();
            }
        }

        public void GetTeachersSubjectsWithClassrooms()
        {
            foreach (var item in _schedule.Where(item => item.Teacher.Id == 30).Select(item => item.ClassroomSubject))
            {
                Console.WriteLine($"{item.Subject.Name} => {item.Classroom.RoomNumber}");
            }
        }

        public void GetSubjectsForEachTeacher()
        {
            var subjects = _schedule.GroupBy(item => item.Teacher, new TeacherEqualityComparer())
            .Select(item => new
            {
                Teacher = item.Key,
                Subjects = item.Select(item => item.ClassroomSubject.Subject).ToList()
            });

            foreach (var teacherSubjects in subjects)
            {
                Console.WriteLine($"Teacher: {teacherSubjects.Teacher.Name}");
                foreach (var subject in teacherSubjects.Subjects)
                {
                    Console.WriteLine($"- {subject.Name}");
                }
            }
        }

        public void GetTeacherWithMaxSubjects()
        {
            var teacher = _schedule.GroupBy(item => item.Teacher, new TeacherEqualityComparer())
                .Select(group => new { Teacher = group.Key, SubjectCount = group.Count() })
                .OrderByDescending(item => item.SubjectCount)
                .FirstOrDefault() ?? throw new ArgumentException("Exception during generation");

            Console.WriteLine(teacher.Teacher.Name);
        }

        public void GetTeachersOrderByAlphabet()
        {
            var teachers = _schedule.Select(item => item).Select(item => new
            {
                Teacher = item.Teacher.Name
            }).Distinct().OrderBy(item => item.Teacher);

            foreach (var item in teachers)
            {
                Console.WriteLine(item.Teacher);
            }
        }

        public void GetScheduleItemsCount()
        {
            int count = _schedule.Where(item => item.Date >= DateTime.UtcNow.AddDays(25)).Count();
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

        public void GetCountCathedrasOnFmm()
        {
            var cathedrasOnFmm = (from catedra in _cathedras
                                  join faculcy in _faculcies on catedra.FaculcyId equals faculcy.Id
                                  select new { Cathedra = catedra, Faculcy = faculcy })
                       .GroupBy(item => item.Faculcy.Title)
                       .Where(item => item.Key == "ФММ")
                       .Select(item => new { Count = item.Count() }).First();
            Console.WriteLine(cathedrasOnFmm);
        }

        public void UnionSchedules()
        {
            var unionSchedule = _schedule.Union(_copySchedule, new ScheduleItemComparer());
            foreach (var item in unionSchedule)
            {
                item.Print();
            }
        }

        public void ExceptItemsSchedule()
        {
            var excludeItems = _schedule.Except(_schedule.Take(99), new ScheduleItemComparer());
            Console.WriteLine("Except: ");
            foreach (var item in excludeItems)
            {
                item.Print();
            }
        }

        public void IntersectScheduleItems()
        {
            var intersectItems = _schedule.Intersect(_copySchedule, new ScheduleItemComparer());
            Console.WriteLine($"Values after intersect: {intersectItems.Count()}");
        }

        public void ExceptItemsOfCopySchedule()
        {
            var exceptItems = _schedule.Except(_copySchedule, new ScheduleItemComparer());
            Console.WriteLine($"Values after except: {exceptItems.Count()}");
        }

        public void CompareSequences()
        {
            Console.WriteLine(_schedule.Take(10).SequenceEqual(_copySchedule.TakeWhile(x => x.Id <= 10), new ScheduleItemComparer()));
        }

        public void SumDigitsInName()
        {
            var sumDigits = (from item in _schedule
                             let digitsSum = item.Teacher.Name
                                 .Where(char.IsDigit)
                                 .Select(c => int.Parse(c.ToString()))
                                 .Sum()
                             select new
                             {
                                 item.Teacher,
                                 DigitsSum = digitsSum
                             }).Take(10);

            foreach (var item in sumDigits)
            {
                Console.WriteLine($"Teacher {item.Teacher.Name} - {item.DigitsSum}");
            }
        }

        public void FindTeachersWith7InTheEnd()
        {
            Console.WriteLine($"Teachers with '7' in the end: {_schedule.Where(item => item.Teacher.Name.EndsWith("7")).Count()}");
        }

        public void GetMaxClassroomNumber()
        {
            var maxNumber = (from item in _schedule
                             select item.ClassroomSubject.Classroom.RoomNumber)
                    .Aggregate((acc, x) => acc > x ? acc : x);
            Console.WriteLine(maxNumber);
        }

        public void IsAnyRoomLessThan100()
        {
            bool result = _schedule.Any(item => item.ClassroomSubject.Classroom.RoomNumber < 100);
            Console.WriteLine(result);
        }

        public void GetLookUpByClassrooms()
        {
            foreach (var item in _schedule.ToLookup(item => item.ClassroomSubject.ClassroomId).Take(5))
            {
                Console.WriteLine($"{item.Key} - {item.Count()}");
            }
        }
    }
}
