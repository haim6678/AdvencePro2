using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi
{
    public class PreMultiViewModel : INotifyPropertyChanged
    {
        public PreMultiModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public PreMultiViewModel(PreMultiModel mod)
        {
            model = mod;
            model.PropertyChanged += Model_PropertyChanged;
            mod.GetList();
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        public void JoinClick()
        {
            model.StartGame("join");
        }

        public void Start()
        {
            //todo check if he input the fields or it's empty!
            model.StartGame("start");
        }

        #region properties

        public ObservableCollection<string> VM_List
        {
            get { return model.List; }
            set { model.List = value; }
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

        #endregion
    }
}