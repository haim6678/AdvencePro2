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


        public event SinglePlayerModel.GameOverHandler GameOver
        {
            add
            {
                model.GameOver += value;
            }
            remove
            {
                model.GameOver -= value;
            }
        }

        public Maze VM_Maze
        {
            get { return model.Maze; }
        }

        public Position VM_Position
        {
            get { return model.Position; }
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

        public void Solve()
        {
            model.Solve();
        }

        public void Restart()
        {
            model.Restart();
        }
    }
}