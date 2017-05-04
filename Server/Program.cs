using MazeLib;
using Server.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        /// <summary>
        /// the main function
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            string portNumber = ConfigurationManager.AppSettings["PortNum"];
            int port = int.Parse(portNumber);
            Server s = new Server(port, new ClientHandler());
            s.Start();
        }
    }
}
