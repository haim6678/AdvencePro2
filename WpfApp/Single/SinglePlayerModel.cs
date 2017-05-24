using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MazeLib;
using SharedData;
using WpfApp.Settings;
using WpfApp.Communication;
using Newtonsoft.Json;

namespace WpfApp
{
    /// <summary>
    ///  single player game model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SinglePlayerModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The message.</param>
        public delegate void GameOverHandler(string message);
        /// <summary>
        /// Occurs when [game over].
        /// </summary>
        public event GameOverHandler GameOver;

        /// <summary>
        /// The solving
        /// </summary>
        private volatile bool solving;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerModel"/> class.
        /// </summary>
        /// <param name="m">The m.</param>
        public SinglePlayerModel(Maze m)
        {
            this.Maze = m;
            this.Position = this.Maze.InitialPos;
            solving = false;
        }

        #region Solve

        /// <summary>
        /// Solves this instance.
        /// </summary>
        public void Solve()
        {
            string cmd = SettingsManager.ReadSetting(SettingName.SearchAlgorithm);
            string ip = SettingsManager.ReadSetting(SettingName.IP);
            int port = int.Parse(SettingsManager.ReadSetting(SettingName.Port));

            Communicator c = new Communicator(ip, port);
            cmd = string.Format("solve {0} {1}", Maze.Name, cmd);
            c.SendMessage(cmd);
            cmd = c.ReadMessage();
            c.Dispose();

            Message m = Message.FromJSON(cmd);
            if (m.MessageType != MessageType.CommandResult)
                return;

            CommandResult res = CommandResult.FromJSON(m.Data);
            if (res.Command != Command.Solve)
                return;

            solving = true;
            MazeSolution sol = MazeSolution.FromJSON(res.Data);
            new Task(() => AnimateSolution(sol)).Start();
        }

        /// <summary>
        /// Animates the solution.
        /// </summary>
        /// <param name="sol">The sol.</param>
        private void AnimateSolution(MazeSolution sol)
        {
            Position = Maze.InitialPos;
            string str = sol.Solution;
            for (int i = 0; i < str.Length; i++)
            {
                Direction d;
                switch (str[i])
                {
                    //go left
                    case '0':
                        d = Direction.Left;
                        break;
                    //go right
                    case '1':
                        d = Direction.Right;
                        break;
                    //go up
                    case '2':
                        d = Direction.Up;
                        break;
                    //go down          
                    case '3':
                        d = Direction.Down;
                        break;
                    default:
                        d = Direction.Unknown;
                        break;
                }
                Move(d);
                Thread.Sleep(500);
            }

            GameOver?.Invoke("The maze has been solved :)");
        }

        #endregion

        #region properties

        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;
        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze
        {
            get { return maze; }
            set
            {
                this.maze = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Maze"));
            }
        }

        /// <summary>
        /// The position
        /// </summary>
        private Position position;
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position
        {
            get { return position; }
            set
            {
                Position p = (Position) value;
                if ((p.Row != position.Row) || (p.Col != position.Col))
                {
                    this.position = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Position"));
                }
            }
        }

        #endregion

        #region moveLogic

        /// <summary>
        /// Handles the movement.
        /// </summary>
        /// <param name="d">The d.</param>
        public void HandleMovement(Direction d)
        {
            if (solving)
                return;

            Move(d);

            if (Position.Col == Maze.GoalPos.Col && Position.Row == Maze.GoalPos.Row)
            {
                // handle win
                GameOver?.Invoke("Congratulations! You won!");
            }
        }

        /// <summary>
        /// Moves the specified d.
        /// </summary>
        /// <param name="d">The d.</param>
        private void Move(Direction d)
        {
            Position pos = GetEstimatedPosition(Position, d);
            if (pos.Col < 0 || pos.Col >= Maze.Cols)
                return;
            if (pos.Row < 0 || pos.Row >= Maze.Rows)
                return;
            if (Maze[pos.Row, pos.Col] == CellType.Wall)
                return;

            Position = pos;
        }

        #endregion

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            Position = Maze.InitialPos;
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
    }
}