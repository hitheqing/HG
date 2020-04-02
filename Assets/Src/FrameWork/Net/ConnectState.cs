using System.Net.Sockets;

namespace HG
{
    public class ConnectState
    {
        public Socket Socket { get; private set; }
        public object Data { get; private set; }
        public ConnectState(Socket socket, object data = null)
        {
            Socket = socket;
            Data = data;
        }
    }
}