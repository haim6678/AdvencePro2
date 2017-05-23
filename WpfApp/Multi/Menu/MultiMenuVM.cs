using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Multi.Menu
{
    public class MultiMenuVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MultiMenuModel model;
        public MultiMenuVM(MultiMenuModel model)
        {
            this.model = model;
            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        public List<string> VM_GameList
        {
            get { return model.GameList; }
        }

        public string VM_MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value;}
        }

        public uint VM_MazeRows
        {
            get { return model.MazeRows; }
            set { model.MazeRows = value; }
        }

        public uint VM_MazeColumns
        {
            get { return model.MazeColumns; }
            set { model.MazeColumns = value; }
        }

        public string GetCommand()
        {
            return model.CommandToSend;
        }

        public void RefreshList()
        {
            model.UpdateAvailableGames();
        }

        public void SetStartCommand()
        {
            model.SetStartCommand();
        }

        public void SetJoinCommand(string gameName)
        {
            model.SetJoinCommand(gameName);
        }
    }
}
