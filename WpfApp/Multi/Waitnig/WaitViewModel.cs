using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi.Waitnig
{
    public class WaitViewModel
    {
        public delegate void Notify(string s);

        public event Notify Finished;
        public WatinModel model;

        public WaitViewModel()
        {
            model = new WatinModel();
        }

        public void GotMessage()
        {
            Finished?.Invoke(model.message);
        }
    }
}