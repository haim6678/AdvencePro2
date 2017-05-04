using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// the class that start the client
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int port = int.Parse(ConfigurationManager.AppSettings["PortNum"]);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            TcpClient c = new TcpClient();
            Console.WriteLine("Connecting to the server at 127.0.0.1:{0}", port);
            c.Connect(ep);
            Console.WriteLine("Connected.");
            Handler handler = new Handler(c);
            handler.Handle();
        }
    }
}