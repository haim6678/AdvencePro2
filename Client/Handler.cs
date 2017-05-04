using SharedData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Handler
    {
        private TcpClient client;
        private bool KeepCom;
        private BinaryWriter writer;
        private BinaryReader reader;


        public Handler(TcpClient c)
        {
            client = c;
            NetworkStream stream = client.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
            KeepCom = true;
        }

        public void Handle()
        {
            Action a = new Action(Listen);
            Task task = new Task(a);
            task.Start();

            Console.WriteLine("Please enter command...");
            string s = Console.ReadLine();

            while (KeepCom)
            {
                writer.Write(s);
                writer.Flush();
                s = Console.ReadLine();
            }

            task.Wait();
        }

        public void Listen()
        {
            Message msg;
            try
            {
                do
                {
                    string s = reader.ReadString();
                    msg = Message.FromJSON(s);
                    if (msg.MessageType == MessageType.CommandResult)
                    {
                        CommandResult result = CommandResult.FromJSON(msg.Data);
                        KeepCom = result.KeepConnection;
                        Console.WriteLine(result.Data);
                    }
                    else if (msg.MessageType == MessageType.Notification)
                    {
                        Notification notif = Notification.FromJSON(msg.Data);
                        switch (notif.NotificationType)
                        {
                            case Notification.Type.GameStarted:
                                //then maze is sent
                                Console.WriteLine(notif.Data);
                                break;
                            case Notification.Type.GameOver:
                                Console.WriteLine(notif.Data);
                                KeepCom = false;
                                break;
                            case Notification.Type.PlayerMoved:
                                // moveUpdate object was sent
                                Console.WriteLine(notif.Data);
                                break;
                        }
                    }
                } while (KeepCom);
            }
            catch (Exception e)
            {
            }
            Console.WriteLine("Connection is closed. Press any key to continue...");
        }
    }
}