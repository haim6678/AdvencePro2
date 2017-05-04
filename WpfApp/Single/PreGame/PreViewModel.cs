using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Single.PreGame
{

  
    public class PreViewModel
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }

        public delegate void Notify();

        public event Notify Notif;

        private PreSingleModel model;
        public PreViewModel()
        {
            model = new PreSingleModel();
            Width = ConfigurationManager.AppSettings["Width"];
            Height = ConfigurationManager.AppSettings["Height"];
        }

        public void PressedOk()
        {
            //todo if didnt enter name - handle

            Notif?.Invoke();
        }
    }


    
}
