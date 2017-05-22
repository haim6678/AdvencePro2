using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MazeLib;

namespace WpfApp
{
    public class SinglePlayerVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SinglePlayerModel model;

        public SinglePlayerVM(SinglePlayerModel mod)
        {
            model = mod;
            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        #region properties

        public Maze VM_Maze
        {
            get { return model.Maze; }
        }

        public Position VM_Position
        {
            get { return model.Position; }
        }

        #endregion

        #region Movemonet

        public void NewLocation(Position p)
        {
            //todo draw the new location
        }

        public void HandleMovement(Key k)
        {
            Direction d = Direction.Unknown;
            switch (k)
            {
                case Key.Left:
                    d = Direction.Left;
                    break;
                case Key.Up:
                    d = Direction.Up;
                    break;
                case Key.Down:
                    d = Direction.Down;
                    break;
                case Key.Right:
                    d = Direction.Right;
                    break;
                default:
                    return;
            }
            model.HandleMovement(d);
        }

        #endregion

        #region solve

        public void Solve()
        {
            model.Solve();
        }

        #endregion

        #region resatrt

        public void Restart()
        {
            // TODO : add confirmation
            model.Restart();
        }

        #endregion
    }
}