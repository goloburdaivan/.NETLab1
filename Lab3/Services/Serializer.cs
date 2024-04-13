using System.Xml.Serialization;

namespace Lab1.Services
{
    public class Serializer
    {
        public static void SerializeToXml<T>(T[] objects, string fileName)
        {
            var serializer = new XmlSerializer(typeof(T[]));
            using (var stream = new StreamWriter(fileName))
            {
                serializer.Serialize(stream, objects);
            }
        }
    }
}
