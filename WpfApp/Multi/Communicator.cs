using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi
{
    public class Communicator
    {
        private TcpClient client;
        public BinaryReader reader { get; }
        public BinaryWriter writer { get; }

        public delegate void ReceivedData(string s);

        public event ReceivedData Received;

        public Communicator(string port, string ip)
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                client = new TcpClient();
                client.Connect(ep);
                NetworkStream strem = client.GetStream();
                reader = new BinaryReader(strem);
                writer = new BinaryWriter(strem);
            }
            catch (Exception e)
            {
                //todo how to handle?
            }
        }

        public void Send(string data)
        {
            writer.Write(data);
        }

        public void Listen()
        {
            string s;
            try
            {
                s = reader.ReadString();
                Received(s);
                //todo that's it?
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void StopListenning()
        {
            this.client.Close();
        }


    }
}