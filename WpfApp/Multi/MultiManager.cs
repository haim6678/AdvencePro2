using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi
{
    class MultiManager
    {
        private Communicator com { get; set; }

        public delegate void Notify();

        public event Notify NotifyFinish;

        public MultiManager(string port, string ip)
        {
            com = new Communicator(port, ip);
        }

        public void Start()
        {
            PreMultiWindow window = new PreMultiWindow(com);
            window.NotifyStart += StartNewMulti;
            window.Show();
        }

        private void StartNewMulti(string s)
        {
            MultiView multi = new MultiView(com, s);
            multi.NotifyFinish += FinishGame; //todo close communicator
            multi.Show();
        }

        private void FinishGame()
        {
            com.StopListenning();
            NotifyFinish?.Invoke();
        }
    }
}