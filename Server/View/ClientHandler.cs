using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData;

namespace Server.View
{
    /// <summary>
    /// the class that handles the client and
    /// talk to him.
    /// </summary>
    /// <seealso cref="Server.View.IClientHandler" />
    /// <seealso cref="Server.View.IClientNotifier" />
    class ClientHandler : IClientHandler, IClientNotifier
    {
        private Controller.Controller controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        public ClientHandler()
        {
            this.controller = new Controller.Controller(this);
        }

        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(Client client)
        {
            Task t = new Task(() =>
            {
                try
                {
                    bool keepCom;
                    do
                    {
                        Console.WriteLine("Waiting for message from {0}", client.ToString());
                        string command = client.ReadMessage();
                        CommandResult res = controller.ExecuteCommand(command, client);
                        keepCom = res.KeepConnection;
                        client.SendCommandResult(res);
                    } while (keepCom && client.Connected);
                    Console.WriteLine("Closing connection with {0}.", client.ToString());
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not reach {0}, closing the connection.", client.ToString());
                }
            });
            t.Start();
        }

        /// <summary>
        /// Notifies the client back.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="notif">The notif.</param>
        public void NotifyClient(IClient client, Notification notif)
        {
            Client c = client as Client;
            if (c == null)
                return;
            try
            {
                c.SendNotification(notif);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not reach {0}, closing connection...", client.ToString());
                c.Close();
            }
        }
    }
}
