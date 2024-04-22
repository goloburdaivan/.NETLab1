using Lab4.LoadBalancer;
using Lab4.Server;

namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args) {
            Client client = new();
            client.TestClient();

            ServerTests serverTests = new ServerTests();
            serverTests.Test();
        }
    }
}
