using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class PreMultiModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Communicator com;

        public delegate void Notify(string s);

        public event Notify NotifyStart;


        public PreMultiModel(Communicator co)
        {
            com = co;
            com.Received += ReceiveList;
        }

        private void ReceiveList(string s)
        {
            //todo convert s to list and set this list
        }

        public void GetList()
        {
            com.Send("list");
            com.Listen();
        }


        public void StartGame(string s)
        {
            NotifyStart?.Invoke(s);
        }

        #region properties

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private string width;

        public string Width
        {
            get { return width; }
            set
            {
                if (value != width)
                {
                    width = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private string height;

        public string Height
        {
            get { return height; }
            set
            {
                if (value != height)
                {
                    height = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
                }
            }
        }

        private ObservableCollection<string> list;

        public ObservableCollection<string> List
        {
            get { return list; }
            set
            {
                if (!value.Equals(list))
                {
                    list = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("List"));
                }
            }
        }

        #endregion
    }
}