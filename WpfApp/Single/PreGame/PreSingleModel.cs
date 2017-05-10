using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Single.PreGame
{
    public class PreSingleModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void Notify();

        public event Notify NotifStart;

        public PreSingleModel()
        {
            this.Width = ConfigurationManager.AppSettings["Width"];
            this.Height = ConfigurationManager.AppSettings["Height"];
        }

        private string width;

        public string Width
        {
            get { return width; }
            set
            {
                if (this.width != value)
                {
                    this.width = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Height));
                }
            }
        }

        public void Start()
        {
            NotifStart?.Invoke();
        }
    }
}