using System.Diagnostics.CodeAnalysis;
using Lab1.Models;

namespace Lab1.Comparators
{
    public class ScheduleItemComparer : IEqualityComparer<ScheduleItem>
    {
        public bool Equals(ScheduleItem? x, ScheduleItem? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ScheduleItem obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
