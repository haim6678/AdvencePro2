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
    /// <summary>
    /// in charge of the multi logic
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IDisposable" />
    public class GameModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public delegate void GameStartedHandler();
        /// <summary>
        /// Occurs when [game started].
        /// </summary>
        public event GameStartedHandler GameStarted;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason">The reason.</param>
        public delegate void GameOverHandler(string reason);
        /// <summary>
        /// Occurs when [game over].
        /// </summary>
        public event GameOverHandler GameOver;

        // events:  game started:
        //          game finished(reason):
        //              - win of player
        //              - exit of player
        /// <summary>
        /// The COM
        /// </summary>
        private Communicator com;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        /// <param name="commandToStart">The command to start.</param>
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


        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
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

        /// <summary>
        /// My position
        /// </summary>
        private Position myPos;
        /// <summary>
        /// Gets my position.
        /// </summary>
        /// <value>
        /// My position.
        /// </value>
        public Position MyPosition
        {
            get { return myPos; }
            private set
            {
                myPos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyPosition"));
            }
        }

        /// <summary>
        /// The other position
        /// </summary>
        private Position otherPos;
        /// <summary>
        /// Gets the other position.
        /// </summary>
        /// <value>
        /// The other position.
        /// </value>
        public Position OtherPosition
        {
            get { return otherPos; }
            private set
            {
                otherPos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OtherPosition"));
            }
        }

        /// <summary>
        /// Handles the movement.
        /// </summary>
        /// <param name="d">The d.</param>
        public void HandleMovement(Direction d)
        {
            com.SendMessage("play " + d.ToString().ToLower());
        }

        /// <summary>
        /// Datas the received.
        /// </summary>
        /// <param name="data">The data.</param>
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

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="n">The n.</param>
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

        /// <summary>
        /// Handles the command result.
        /// </summary>
        /// <param name="res">The resource.</param>
        /// <exception cref="WpfApp.Multi.Game.GameNotStartedException"></exception>
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

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            string name = (Maze != null) ? Maze.Name : "thisgame";
            com.SendMessage("close " + name);
        }

        /// <summary>
        /// Gets the estimated position.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
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


