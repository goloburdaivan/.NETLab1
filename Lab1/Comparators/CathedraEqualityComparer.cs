using System.Diagnostics.CodeAnalysis;
using Lab1.Models;

namespace Lab1.Comparators
{
    internal class CathedraEqualityComparer : IEqualityComparer<Cathedra>
    {
        public bool Equals(Cathedra? x, Cathedra? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Cathedra obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
