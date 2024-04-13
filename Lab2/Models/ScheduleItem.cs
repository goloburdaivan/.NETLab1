using Lab1.Interfaces;

namespace Lab1.Models
{
    public class ScheduleItem : IPrintable
    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; }
        public ClassroomSubject ClassroomSubject { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
        public DateTime Date { get; set; }

        public void Print()
        {
            Console.WriteLine($"Schedule item id: {Id}");
            Console.WriteLine($"Teacher: Id = {Teacher.Id}, Name = {Teacher.Name}, Surname = {Teacher.Surname}");
            Console.WriteLine($"Classroom: Id = {ClassroomSubject.ClassroomId}");
            Console.WriteLine($"Subject: Id = {ClassroomSubject.SubjectId}");
            Console.WriteLine($"Groups: {string.Join(',', Groups.Select(group => group.Code))}");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Cathedra: {Teacher.Cathedra.Title}");
            Console.WriteLine($"Faculcy: {Teacher.Cathedra.Faculcy.Title}");
        }
    }
}
