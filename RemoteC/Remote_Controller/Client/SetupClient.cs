using System.Net;
using System.Net.Sockets;

namespace Remote_Controller
{
    class SetupClient
    {
        public static void Setup(string ip, int port)
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            Socket sock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            sock.Connect(ep);
            sock.Close();
        }
    }
}
