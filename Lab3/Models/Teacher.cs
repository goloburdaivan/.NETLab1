namespace Lab1.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CathedraId { get; set; }
        public Cathedra Cathedra { get; set; }
    }
}
