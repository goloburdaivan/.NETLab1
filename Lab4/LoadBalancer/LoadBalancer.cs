namespace Lab4.LoadBalancer
{
    public class LoadBalancer
    {
        private IList<Server> _servers = new List<Server>();
        private static LoadBalancer? instance = null;

        private LoadBalancer()
        {

        }

        public static LoadBalancer GetInstance()
        {
            if (instance == null)
            {
                instance = new LoadBalancer();
            }

            return instance;
        }

        public void AddServer(Server server)
        {
            _servers.Add(server);
        }

        public string SendRequest(string request)
        {
            string response = "";
            int serverIndex = 0;


            for (int i = 0; i < _servers.Count; i++)
            {
                if (_servers[i].IsOnline)
                {
                    response = _servers[i].ProcessRequest(request);
                    serverIndex = i + 1;
                    break;
                }
            }

            if (response == string.Empty)
            {
                return "Sorry, no servers online :(";
            }

            Console.WriteLine($"Request is sent with server {serverIndex}");

            return response;
        }

        public int ServersCount()
        {
            return _servers.Count;
        }
    }
}
