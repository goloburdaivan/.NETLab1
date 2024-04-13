using Lab1.Models;

namespace Lab1.Generators
{
    public class ScheduleGenerator
    {

        private readonly Schedule _schedule;
        private readonly Random _random;
        private readonly FaculcyGenerator _faculcyGenerator;

        public ScheduleGenerator(Schedule schedule, Random random, FaculcyGenerator faculcyGenerator)
        {
            _schedule = schedule;
            _random = random;
            _faculcyGenerator = faculcyGenerator;
        }
        public Schedule GenerateSchedule(int Size)
        {
            for (int i = 0; i < Size; i++)
            {
                var classRoom = new Classroom { Id = _random.Next(1, 100), RoomNumber = _random.Next(100, 200), SubjectId = _random.Next(1, 100) };
                var subject = new Subject { Id = _random.Next(1, 100), Name = "Subject" + _random.Next(1, 100), ClassroomId = _random.Next(1, 100) };
                var faculcy = _faculcyGenerator.Faculcies[_random.Next(0, 10)];
                var cathedra = new Cathedra
                {
                    Id = _random.Next(1, 100),
                    Title = $"Cathedra {_random.Next(1, 100)}",
                    Faculcy = faculcy,
                    FaculcyId = faculcy.Id
                };
                ScheduleItem item = new ScheduleItem
                {
                    Id = i + 1,
                    Teacher = new Teacher
                    {
                        Id = _random.Next(1, 100),
                        Name = "Teacher" + _random.Next(1, 100),
                        Surname = "Surname" + _random.Next(1, 100),
                        CathedraId = _random.Next(1, 10),
                        Cathedra = cathedra
                    },
                    ClassroomSubject = new ClassroomSubject { ClassroomId = classRoom.Id, SubjectId = subject.Id, Classroom = classRoom, Subject = subject },
                    Groups = new List<Group>(),
                    Date = DateTime.Now.AddDays(_random.Next(1, 30))
                };

                int groupCount = _random.Next(1, 4);
                for (int j = 0; j < groupCount; j++)
                {
                    item.Groups.Add(new Group { Id = _random.Next(1, 100), Code = "Group" + _random.Next(1, 10) });
                }

                _schedule.AddScheduleItem(item);
            }

            return _schedule;
        }
    }
}
