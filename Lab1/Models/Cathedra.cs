namespace Lab1.Models
{
    public class Cathedra
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int FaculcyId { get; set; }
        public Faculcy Faculcy { get; set; }
    }
}
