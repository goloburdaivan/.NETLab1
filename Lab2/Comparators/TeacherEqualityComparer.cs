using System.Diagnostics.CodeAnalysis;
using Lab1.Models;

namespace Lab1.Comparators
{
    internal class TeacherEqualityComparer : IEqualityComparer<Teacher>
    {
        public bool Equals(Teacher? x, Teacher? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Teacher obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
