using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp.Multi.Game
{
    /// <summary>
    /// the multi view model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IDisposable" />
    public class GameVM : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The model
        /// </summary>
        private GameModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GameVM(GameModel model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
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

        /* event hookup of gameStarted and GameOver */
        /// <summary>
        /// Occurs when [game started].
        /// </summary>
        public event GameModel.GameStartedHandler GameStarted
        {
            add
            {
                model.GameStarted += value;
            }
            remove
            {
                model.GameStarted -= value;
            }
        }
        /// <summary>
        /// Occurs when [game over].
        /// </summary>
        public event GameModel.GameOverHandler GameOver
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
        /// Gets the vm my position.
        /// </summary>
        /// <value>
        /// The vm my position.
        /// </value>
        public Position VM_MyPosition
        {
            get { return model.MyPosition; }
        }

        /// <summary>
        /// Gets the vm other position.
        /// </summary>
        /// <value>
        /// The vm other position.
        /// </value>
        public Position VM_OtherPosition
        {
            get { return model.OtherPosition; }
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
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            model.CloseGame();
        }

        #region IDisposable Support
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).this.model = model;
                    this.model.PropertyChanged -= Model_PropertyChanged;
                    this.model.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GameVM() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
