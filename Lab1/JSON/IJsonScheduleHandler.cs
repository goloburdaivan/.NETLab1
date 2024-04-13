using Lab1.Models;

namespace Lab1.JSON
{
    public interface IJsonScheduleHandler
    {
        void SerializeFile(List<ScheduleItem> obj, Stream file);
        List<ScheduleItem> DeserializeFile(Stream file);
    }
}
