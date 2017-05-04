using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MazeLib;
using Wpf.Single.Confirm;
using System.Windows;

namespace Wpf
{
    class SinglePlayerModel
    {
        public delegate void GameEnded(string s);

        public event GameEnded HandleFinish;

        public delegate void GotMessage();

        public event GotMessage Message;

        public string MassageData { get; set; }

        public SinglePlayerModel()
        {
        }

        private void Communicate(string s)
        {
            string portNumber = ConfigurationManager.AppSettings["PortNum"];
            string ipNumber = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(portNumber), int.Parse(ipNumber));
            TcpClient client = new TcpClient();
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(s);
            s = reader.ReadString();
            MassageData = s;
            client.Close();
        }

        public void start(string name, string width, string height)
        {
            Communicate(name + " " + width + " " + height);
            Message?.Invoke();
        }


        public void Solve(string s)
        {
            string str = ConfigurationManager.AppSettings["SearchAlgo"];
            Communicate("solve " + s + " " + str);

            Message?.Invoke();
        }

        

      
    }
}