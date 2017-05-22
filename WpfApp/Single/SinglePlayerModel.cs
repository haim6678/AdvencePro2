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
using System.Windows;
using SharedData;

namespace WpfApp
{
    public class SinglePlayerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void GameOverHandler(string message);
        public event GameOverHandler GameOver;

        public SinglePlayerModel(Maze m)
        {
            this.Maze = m;
            this.Position = this.Maze.InitialPos;
        }

        #region Solve

        public void Solve()
        {
            string str = ConfigurationManager.AppSettings["SearchAlgo"];
            //Communicate("solve " + Maze.Name + " " + str);
            //todo what string to represent when the user pressed solve?
        }

        private void AnimateSolution(string s)
        {
            string str = s;
            string[] arr = str.Split(',');
            str = arr[2];
            arr = str.Split(':');
            str = arr[1];

            str.Replace("\r", "");
            str.Replace("\n", "");

            Console.WriteLine(str);
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    //go left
                    case '0':
                        this.Position = new Position(this.position.Row, this.position.Col - 1);
                        break;
                    //go right
                    case '1':
                        this.Position = new Position(this.position.Row, this.position.Col + 1);
                        break;
                    //go up
                    case '2':
                        this.Position = new Position(this.position.Row - 1, this.position.Col);
                        break;
                    //go down          
                    case '3':
                        this.Position = new Position(this.position.Row + 1, this.position.Col);
                        break;
                    default:
                        break;
                }
                System.Threading.Thread.Sleep(500);
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Maze")); //todo check
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Position")); //todo check
                }
            }
        }

        #endregion

        #region moveLogic

        public void HandleMovement(Direction d)
        {
            Position pos = GetEstimatedPosition(Position, d);
            if (pos.Col < 0 || pos.Col >= Maze.Cols)
                return;
            if (pos.Row < 0 || pos.Row >= Maze.Rows)
                return;
            if (Maze[pos.Row, pos.Col] == CellType.Wall)
                return;

            Position = pos;
            if (Position.Col == Maze.GoalPos.Col && Position.Row == Maze.GoalPos.Row)
            {
                // handle win
                MessageBox.Show("Congratulations! You won!", "You Won!", MessageBoxButton.OK, MessageBoxImage.None);
                
            }
        }

        private Position HandleMove(Key k)
        {
            Position pos = new Position(-1, -1);

            switch (k)
            {
                case Key.Down:
                    if ((position.Row + 1 < maze.Rows) &&
                        (maze[position.Row + 1, position.Col] == CellType.Free))
                    {
                        pos = new Position(position.Row + 1, position.Col);
                    }
                    break;
                case Key.Up:
                    if ((position.Row - 1 >= 0) && (maze[position.Row - 1, position.Col] == CellType.Free))
                    {
                        pos = new Position(position.Row - 1, position.Col);
                    }
                    break;
                case Key.Left:
                    if ((position.Col - 1 >= 0) && (maze[position.Row, position.Col - 1] == CellType.Free))
                    {
                        pos = new Position(position.Row, position.Col - 1);
                    }
                    break;
                case Key.Right:
                    if ((position.Col + 1 < maze.Cols) &&
                        (maze[position.Row, position.Col + 1] == CellType.Free))
                    {
                        pos = new Position(position.Row, position.Col + 1);
                    }
                    break;
                default:
                    pos = new Position(-1, -1);
                    break;
            }
            return pos;
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