namespace Lab4.LoadBalancer
{
    public class Server
    {
        public bool IsOnline { get; set; } = true;
        public static int Instances = 0;
        public string Address { get; set; }
        public int Port { get; set; }
        public delegate string OnRequest(string request);
        public OnRequest RequestHandler;

        public Server(string address, int port) 
        {
            Address = address;
            Port = port;
            Instances++;
        }

        public string ProcessRequest(string request)
        {
            return RequestHandler.Invoke(request) ?? "";
        }

        ~Server() 
        {
            Console.WriteLine("Server is dead");
            Instances--;
        }
    }
}
