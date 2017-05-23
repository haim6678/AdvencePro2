using MazeLib;
using SharedData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Communication;
using WpfApp.Settings;

namespace WpfApp.Multi.Game
{
    public class GameModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void GameStartedHandler();
        public event GameStartedHandler GameStarted;

        public delegate void GameOverHandler(string reason);
        public event GameOverHandler GameOver;

        // events:  game started:
        //          game finished(reason):
        //              - win of player
        //              - exit of player
        private Communicator com;

        public GameModel(string commandToStart)
        {
            this.maze = null;

            this.com = new Communicator(
                SettingsManager.ReadSetting(SettingName.IP),
                int.Parse(SettingsManager.ReadSetting(SettingName.Port)));
            this.com.DataReceived += DataReceived;
            this.com.StartListening();

            this.com.SendMessage(commandToStart);
        }


        private Maze maze;
        public Maze Maze
        {
            get { return this.maze; }
            private set
            {
                this.maze = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Maze"));
                MyPosition = Maze.InitialPos;
                OtherPosition = Maze.InitialPos;
            }
        }

        private Position myPos;
        public Position MyPosition
        {
            get { return myPos; }
            private set
            {
                myPos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyPosition"));
            }
        }

        private Position otherPos;
        public Position OtherPosition
        {
            get { return otherPos; }
            private set
            {
                otherPos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OtherPosition"));
            }
        }

        public void HandleMovement(Direction d)
        {
            com.SendMessage("play " + d.ToString().ToLower());
        }

        private void DataReceived(string data)
        {
            Debug.WriteLine("Data Received: " + data);
            Message m = Message.FromJSON(data);
            if (m.MessageType == MessageType.CommandResult)
            {
                CommandResult res = CommandResult.FromJSON(m.Data);
                HandleCommandResult(res);
            }
            else if (m.MessageType == MessageType.Notification)
            {
                Notification n = Notification.FromJSON(m.Data);
                HandleNotification(n);
            }
        }

        private void HandleNotification(Notification n)
        {
            switch (n.NotificationType)
            {
                case Notification.Type.GameStarted:
                    // if game already started
                    if (Maze != null)
                        return;
                    Maze = Maze.FromJSON(n.Data);
                    GameStarted?.Invoke();
                    break;
                case Notification.Type.GameOver:
                    GameOver?.Invoke(n.Data);
                    break;
                case Notification.Type.PlayerMoved:
                    MoveUpdate mu = MoveUpdate.FromJSON(n.Data);
                    OtherPosition = GetEstimatedPosition(OtherPosition, mu.Direction);
                    break;
                default:
                    break;
            }
        }

        private void HandleCommandResult(CommandResult res)
        {
            if (res.Command != Command.Play)
            {
                if (!res.Success)
                    throw new GameNotStartedException(res.Data);
                return;
            }

            if (!res.Success)
                return;

            Direction d = (Direction)Enum.Parse(typeof(Direction), res.Data);
            MyPosition = GetEstimatedPosition(MyPosition, d);
        }

        public void CloseGame()
        {
            com.SendMessage("close " + Maze.Name);
        }

        private static Position GetEstimatedPosition(Position p, Direction d)
        {
            switch (d)
            {
                case Direction.Up:
                    --p.Row;
                    break;
                case Direction.Down:
                    ++p.Row;
                    break;
                case Direction.Left:
                    --p.Col;
                    break;
                case Direction.Right:
                    ++p.Col;
                    break;
            }
            return p;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    com.StopListening();
                    com.DataReceived -= DataReceived;
                    com.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GameModel() {
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


