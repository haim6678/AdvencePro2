using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// the class that is in charge of the server side.
    /// </summary>
    class Server
    {
        private int port;
        private IClientHandler ch;
        private TcpListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
            listener = null;
        }

        /// <summary>
        /// Starts this instance.
        /// gets clients in a loop.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Starting server on port {0}...", port);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            // start listening for connections
            listener.Start();
            Console.WriteLine("Waiting for connections...");

            //gets the clients
            Task listeningTask = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        TcpClient c = listener.AcceptTcpClient();
                        Client client = new Client(c);

                        Console.WriteLine("Got new connection: {0}", client.ToString());
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            Console.WriteLine("Stating the listening task...");
            listeningTask.Start();
            listeningTask.Wait();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            Console.WriteLine("Stopping server...");
            listener.Stop();
        }
    }
}