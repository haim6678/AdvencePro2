using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Multi.Game
{
    public class GameVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private GameModel model;

        public GameVM(GameModel model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }
        
        public Position VM_CurrentPosition
        {
            get { return model.CurrentPosition; }
        } 

        public Position VM_OtherPosition
        {
            get { return model.OtherPosition; }
        }

        public Maze VM_Maze
        {
            get { return model.Maze; }
        }
    }
}
