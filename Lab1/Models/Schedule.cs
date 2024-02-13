using System.Collections;
using Lab1.Interfaces;

namespace Lab1.Models
{
    public class Schedule : IEnumerable<ScheduleItem>, IPrintable
    {
        private readonly List<ScheduleItem> _items = new();

        public void AddScheduleItem(ScheduleItem item)
        {
            _items.Add(item);
        }

        public IEnumerator<ScheduleItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public void Print()
        {
            foreach (var item in _items)
            {
                item.Print();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
