using Lab1.JSON;

namespace Lab1.Factory
{
    public class JsonHandlerFactory
    {
        public static IJsonScheduleHandler CreateJsonHandler(string type)
        {
            switch (type)
            {
                case "JsonDocument":
                    return new JsonDocumentScheduleHandler();
                case "System.Text.Json":
                    return new JsonNetScheduleHandler();
                case "JsonNode":
                    return new JsonNodeScheduleHandler();
                default:
                    throw new ArgumentException("Invalid JSON handler type");
            }
        }
    }
}
