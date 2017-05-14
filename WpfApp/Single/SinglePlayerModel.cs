using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MazeLib;
using WpfApp.Single.Confirm;
using System.Windows;
using SharedData;

namespace WpfApp
{
    public class SinglePlayerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void Notify();

        public event Notify HandleFinish;
        public event Notify HandleMassage;
        public string FinishMassage;
        private string ServerMassage;


        public SinglePlayerModel()
        {
        }

        public void Finish(string s)
        {
            HandleFinish?.Invoke();
        }

        #region communication

        private void Communicate(string s)
        {
            string portNumber = ConfigurationManager.AppSettings["PortNum"];
            string ipNumber = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipNumber), int.Parse(portNumber));
            TcpClient client = new TcpClient();
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(s);
            ServerMassage = reader.ReadString();
            client.Close();
            HandleMassage?.Invoke();
        }

        public void start()
        {
            HandleMassage += StartMaesaage;
            Communicate("generate " + name + " " + width + " " + height);
        }

        private void StartMaesaage()
        {
            HandleMassage += StartMaesaage;
            SharedData.Message msg = SharedData.Message.FromJSON(ServerMassage);

            CommandResult result = CommandResult.FromJSON(msg.Data);
            if (result.Success)
            {
                SetStringMaze(result.Data);
                this.position = maze.InitialPos;
            }
            else
            {
                //todo handle com failed
            }
        }

        public void SetStringMaze(string s)
        {
            Maze m = Maze.FromJSON(s);
            this.Maze = m;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Cols; j++)
                {
                    if (m[i, j] == CellType.Free)
                    {
                        builder.Append("0");
                    }
                    else if ((m[i, j] == CellType.Wall))
                    {
                        builder.Append("1");
                    }
                }
            }
            Position p = m.InitialPos;
            builder[p.Row * m.Cols + p.Col] = '*';
            p = m.GoalPos;
            builder[p.Row * m.Cols + p.Col] = '#';
            Console.WriteLine(m);

            string str = builder.ToString();
            Console.WriteLine(str);
            mazeString = str;
        }

        private string mazeString;

        public string MazeString
        {
            get { return mazeString; }
            set
            {
                if (!mazeString.Equals(value))
                {
                    this.mazeString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeString"));
                }
            }
        }

        public void Restart()
        {
            Position = Maze.InitialPos;
        }

        public void Solve()
        {
            string str = ConfigurationManager.AppSettings["SearchAlgo"];
            Communicate("solve " + name + " " + str);
        }

        #endregion

        #region properties

        private string name;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private string width;

        public string Width
        {
            get { return this.width; }
            set
            {
                if (this.width != value)
                {
                    this.width = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
                }
            }
        }


        private string height;

        public string Height
        {
            get { return this.height; }
            set
            {
                if (this.height != value)
                {
                    this.height = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
                }
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

        #endregion

        #region moveLogic

        public void HandleMovement(Key k)
        {
            Position pos = HandleMove(k);
            if ((pos.Col != -1) && (position.Row != -1))
            {
                Position = pos;
                if ((position.Col == maze.GoalPos.Col)
                    && (position.Row == Maze.GoalPos.Row))
                {
                    FinishMassage = "YOU WON";
                    HandleFinish?.Invoke();
                }
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

        public void BackToMenu()
        {
            HandleFinish?.Invoke();
        }
    }
}