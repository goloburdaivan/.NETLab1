namespace Lab4.LoadBalancer
{
    public class Client
    {
        public void TestClient()
        {
            LoadBalancer lb1 = LoadBalancer.GetInstance();
            LoadBalancer lb2 = LoadBalancer.GetInstance();
            Server server1 = new Server("localhost", 8080);
            Server server2 = new Server("localhost", 8000);
            Server server3 = new Server("8.8.8.8", 80);

            lb1.AddServer(server1);
            lb1.AddServer(server2);
            lb1.AddServer(server3);

            server3.IsOnline = false;

            if (lb1.ServersCount() == lb2.ServersCount())
            {
                Console.WriteLine("Singleton works!");
            }

            RequestBuilder builder =
                new RequestBuilder()
                .AddHeader("Cookies", "expires-at=3600")
                .AddRequestUri("/")
                .AddRequestMethod("POST");
            string request = builder.Build();

            server1.RequestHandler = (string request) =>
            {
                if (request.Contains("POST")) 
                {
                    return "This request contains POST";
                }

                return "This request contains GET";
            };

            server2.RequestHandler = (string request) =>
            {
                if (request.Contains("Cookies"))
                {
                    return "You sent me a request with Cookies!";
                }

                return "Sorry, no cookies in request :(";
            };

            server3.RequestHandler = (string request) =>
            {
                return "I`m pretending dead! Don`t touch me!";
            };

            Console.WriteLine(lb1.SendRequest(request));
            Console.WriteLine(lb2.SendRequest(request));

            if (lb1.SendRequest(request) == lb2.SendRequest(request))
            {
                Console.WriteLine("Singleton works good!");
            }

            server1.IsOnline = false;
            Console.WriteLine("Server 1 is down!");

            Console.WriteLine(lb1.SendRequest(request));
            Console.WriteLine(lb2.SendRequest(request));

            if (lb1.SendRequest(request) == lb2.SendRequest(request))
            {
                Console.WriteLine("Singleton works good!");
            }

            server2.IsOnline = false;
            Console.WriteLine(lb1.SendRequest(request));
            Console.WriteLine(lb2.SendRequest(request));

            if (lb1.SendRequest(request) == lb2.SendRequest(request))
            {
                Console.WriteLine("Singleton works good!");
            }

            server3.IsOnline = true;
            Console.WriteLine(lb1.SendRequest(request));
            Console.WriteLine(lb2.SendRequest(request));

            if (lb1.SendRequest(request) == lb2.SendRequest(request))
            {
                Console.WriteLine("Singleton works good!");
            }
        }
    }
}
