using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Settings
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SettingsModel model;

        public SettingsViewModel(SettingsModel model)
        {
            this.model = model;
            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        public string VM_IP
        {
            get { return model.IP; }
            set { model.IP = value; }
        }


        public ushort VM_Port
        {
            get { return model.Port; }
            set { model.Port = value; }
        }


        public uint VM_MazeWidth
        {
            get { return model.MazeWidth; }
            set { model.MazeWidth = value; }
        }


        public uint VM_MazeHeight
        {
            get { return model.MazeHeight; }
            set { model.MazeHeight = value; }
        }

        public byte VM_SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set { model.SearchAlgorithm = value; }
        }

        public void OkPressed()
        {
            model.SaveSettings();
        }
    }
}