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
    /// <summary>
    ///  single player view model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SinglePlayerVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The model
        /// </summary>
        private SinglePlayerModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerVM"/> class.
        /// </summary>
        /// <param name="mod">The mod.</param>
        public SinglePlayerVM(SinglePlayerModel mod)
        {
            model = mod;
            model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Model control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }


        /// <summary>
        /// Occurs when [game over].
        /// </summary>
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

        /// <summary>
        /// Gets the vm maze.
        /// </summary>
        /// <value>
        /// The vm maze.
        /// </value>
        public Maze VM_Maze
        {
            get { return model.Maze; }
        }

        /// <summary>
        /// Gets the vm position.
        /// </summary>
        /// <value>
        /// The vm position.
        /// </value>
        public Position VM_Position
        {
            get { return model.Position; }
        }

        /// <summary>
        /// Handles the movement.
        /// </summary>
        /// <param name="k">The k.</param>
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

        /// <summary>
        /// Solves this instance.
        /// </summary>
        public void Solve()
        {
            model.Solve();
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            model.Restart();
        }
    }
}