
namespace Lab4
{
    public class Server
    {
        private static Server instance = null;
        public bool IsActive { get; set; } = true;
        private Server() 
        { 
        }

        public static Server GetInstance() 
        { 
            instance ??= new Server();
            return instance; 
        }

    }
}
