using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Annotations;

namespace WpfApp.Single.PreGame
{
    public class PreViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private PreSingleModel model;

        public PreViewModel(PreSingleModel Mod)
        {
            model = new PreSingleModel();
            model = Mod;
            model.PropertyChanged += Model_PropertyChanged;
        }

        public void PressedOk()
        {
            //todo if didnt enter name - handle
            model.Start();
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        public string VM_Width
        {
            get { return model.Width; }
            set { model.Width = value; }
        }

        public string VM_Height
        {
            get { return model.Height; }
            set { model.Height = value; }
        }
        public string VM_Name
        {
            get { return model.Name; }
            set { model.Name = value; }
        }
    }
}