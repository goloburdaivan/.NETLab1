using Lab1.Models;

namespace Lab1.Generators
{
    public class FaculcyGenerator
    {
        public List<Faculcy> Faculcies { get; } = new List<Faculcy>();
        public FaculcyGenerator()
        {
            Faculcies.AddRange(
                new Faculcy[]
                {
                    new Faculcy { Id = 1, Title = "ФІОТ" },
                    new Faculcy { Id = 2, Title = "ФПМ" },
                    new Faculcy { Id = 3, Title = "ФЛ" },
                    new Faculcy { Id = 4, Title = "ФЕЛ" },
                    new Faculcy { Id = 5, Title = "ІХВ" },
                    new Faculcy { Id = 6, Title = "РТФ" },
                    new Faculcy { Id = 7, Title = "ПБФ" },
                    new Faculcy { Id = 8, Title = "ФБМІ" },
                    new Faculcy { Id = 9,  Title = "ФЕА"},
                    new Faculcy { Id = 10, Title = "ФММ" }
                }
            );
        }
    }
}
