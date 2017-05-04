using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Multi
{
    public class PreMultiModel
    {
        public delegate void Notify();
        public event Notify NotifyList ;

        public ObservableCollection<string> list;
        public PreMultiModel()
        {
            
        }

        public void GetList()
        {
            string portNumber = ConfigurationManager.AppSettings["PortNum"];
            string ipNumber = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(portNumber), int.Parse(ipNumber));
            TcpClient client = new TcpClient();
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write("list");
            string s = reader.ReadString();
            //todo convert s tolist

            NotifyList?.Invoke();
        }

    }
}
