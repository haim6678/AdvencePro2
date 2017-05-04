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

namespace WpfApp.Multi
{
    public class PreMultiModel
    {
        public delegate void Notify();

        public event Notify NotifyList;
        private Communicator com;
        public string message { get; set; }
        public ObservableCollection<string> list;

        public PreMultiModel(Communicator co)
        {
            com = co;
            com.Received += ReceiveList;
        }

        private void ReceiveList(string s)
        {
            message = s;
            NotifyList?.Invoke();
        }

        public void GetList()
        {
            com.Send("list");
            com.Listen();
            //todo convert s to list
        }
    }
}