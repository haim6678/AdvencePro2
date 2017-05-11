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

        private PreMultiModel preModel;
        private PreMultiViewModel preVM;
        private PreMultiWindow preView;
        private MultiView multi;
        private MultiModel multiModel;
        private MultiViewModel multiVM;

        public MultiManager(string port, string ip)
        {
            com = new Communicator(port, ip);
            preModel = new PreMultiModel(com);
            preModel.NotifyStart += StartNewMulti;
            preVM = new PreMultiViewModel(preModel);
            preView = new PreMultiWindow(preVM);
        }

        public void Start()
        {
            preView.ShowDialog();
        }

        private void StartNewMulti(string s)
        {
            switch (s)
            {
                case "join":
                    s = s + " " + preModel.Name;
                    break;
                case "start":
                    s = s + " " + preModel.Name + " " + preModel.Width + " " + preModel.Height;
                    //todo width then height or the other way?
                    break;
                default:
                    s = null;
                    break;
            }

            if (s != null)
            {
                multiModel = new MultiModel(com, s);
                multiModel.NotifyFinish += FinishGame;
                multiVM = new MultiViewModel(multiModel);
                MultiView multi = new MultiView(multiVM);

                multi.Show();
            }
        }

        private void FinishGame()
        {
            com.StopListenning();
            //todo display window with detailes on who closed the game 
            //todo the info is in the finish message in the model
            multi.Close();
        }
    }
}