namespace Lab4.LoadBalancer
{
    public class ServerSingleton
    {
        public bool IsOnline { get; set; } = true;
        public static int Instances = 0;
        private static ServerSingleton? _instance = null;
        public string Address { get; set; } = "localhost";
        public int Port { get; set; } = 3306;
        public delegate string OnRequest(string request);
        public OnRequest RequestHandler;

        private ServerSingleton() 
        {
        }

        public static ServerSingleton GetInstance()
        {
            if (_instance == null)
            {
                Instances++;
                _instance = new ServerSingleton();
            }

            return _instance;
        }

        public string ProcessRequest(string request)
        {
            if (!IsOnline)
            {
                return "Sorry! I am offline";
            }

            return RequestHandler.Invoke(request) ?? "";
        }

        ~ServerSingleton() 
        {
            Console.WriteLine("Server is dead");
            Instances--;
        }
    }
}
