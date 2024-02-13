namespace Lab1.Models
{
    public class ClassroomSubject
    {
        public int ClassroomId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Classroom Classroom { get; set; }
    }
}
