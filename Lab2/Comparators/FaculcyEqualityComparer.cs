using System.Diagnostics.CodeAnalysis;
using Lab1.Models;

namespace Lab1.Comparators
{
    internal class FaculcyEqualityComparer : IEqualityComparer<Faculcy>
    {
        public bool Equals(Faculcy? x, Faculcy? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Faculcy obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
