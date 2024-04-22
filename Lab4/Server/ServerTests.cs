using Lab4.LoadBalancer;
using System.Text.Json;

namespace Lab4.Server
{
    public class ServerTests
    {
        public void Test()
        {
            RequestBuilder builder =
                new RequestBuilder()
                .AddHeader("Cookies", "expires-at=3600")
                .AddRequestUri("/")
                .AddRequestMethod("POST");

            string request = builder.Build();
            ServerSingleton server = ServerSingleton.GetInstance();
            ServerSingleton anotherInstance = ServerSingleton.GetInstance();
            server.RequestHandler = (string request) =>
            {
                return "I am stupid server. I do Nothing";
            };

            Console.WriteLine(server.ProcessRequest(request));
            Console.WriteLine(anotherInstance.ProcessRequest(request));

            if (
                ServerSingleton.Instances == 1 
                && server.ProcessRequest(request) == anotherInstance.ProcessRequest(request))
            {
                Console.WriteLine("Singleton v.2 works fine!");
            }

            server.RequestHandler = (string request) =>
            {
                return JsonSerializer.Serialize(server);
            };

            Console.WriteLine(server.ProcessRequest(request));
            Console.WriteLine(anotherInstance.ProcessRequest(request));

            if (
                ServerSingleton.Instances == 1
                && server.ProcessRequest(request) == anotherInstance.ProcessRequest(request))
            {
                Console.WriteLine("Singleton v.2 works fine!");
            }

            server.IsOnline = false;

            Console.WriteLine(server.ProcessRequest(request));
            Console.WriteLine(anotherInstance.ProcessRequest(request));
        }
    }
}
