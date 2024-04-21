namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server server = Server.GetInstance();
            server.IsActive = false;
        }
    }
}