using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi.Waitnig
{
    public class WatinModel
    {
        public delegate void Notify();

        public event Notify RecivedGame;
        public string message { get; set; }
        private Communicator com;

        public WatinModel()
        {
        }
    }
}