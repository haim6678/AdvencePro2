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
    public class SinglePlayerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void GameOverHandler(string message);
        public event GameOverHandler GameOver;

        private volatile bool solving;

        public SinglePlayerModel(Maze m)
        {
            this.Maze = m;
            this.Position = this.Maze.InitialPos;
            solving = false;
        }

        #region Solve

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

        private Maze maze;
        public Maze Maze
        {
            get { return maze; }
            set
            {
                this.maze = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Maze"));
            }
        }

        private Position position;
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

        public void Restart()
        {
            Position = Maze.InitialPos;
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
    }
}