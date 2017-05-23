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
    public class GameVM : INotifyPropertyChanged, IDisposable
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

        /* event hookup of gameStarted and GameOver */
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

        
        public Position VM_MyPosition
        {
            get { return model.MyPosition; }
        } 

        public Position VM_OtherPosition
        {
            get { return model.OtherPosition; }
        }

        public Maze VM_Maze
        {
            get { return model.Maze; }
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

        public void CloseGame()
        {
            model.CloseGame();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

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
