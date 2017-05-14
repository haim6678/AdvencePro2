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
        public event Notify FinishSolve;
        public event Notify HandleMassage;
        public string FinishMassage;
        private string ServerMassage;


        public SinglePlayerModel()
        {
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

        #endregion

        #region StartGame

        public void start()
        {
            HandleMassage += HandleStartMaesaage;
            Communicate("generate " + name + " " + width + " " + height);
        }

        private void HandleStartMaesaage()
        {
            HandleMassage -= HandleStartMaesaage;
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

        private void SetStringMaze(string s)
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

        #endregion

        #region Solve

        public void Solve()
        {
            string str = ConfigurationManager.AppSettings["SearchAlgo"];
            this.HandleMassage += HandleSolveMassage;
            Communicate("solve " + name + " " + str);
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
            FinishMassage = "YOU USED SOLVE COMMAND"; //TODO what to do
            FinishSolve?.Invoke();
        }

        private void HandleSolveMassage()
        {
            HandleMassage -= HandleSolveMassage;
            SharedData.Message message = Message.FromJSON(ServerMassage);
            CommandResult commandResult = CommandResult.FromJSON(message.Data);
            if (!commandResult.Success)
            {
                //todo handle fail
            }
            Thread t2 = new Thread(delegate() { AnimateSolution(commandResult.Data); });
            t2.SetApartmentState(ApartmentState.STA);

            t2.Start();
        }

        #endregion

        #region properties

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

        #region finish

        public void BackToMenu()
        {
            HandleFinish?.Invoke();
        }

        #endregion

        public void Restart()
        {
            Position = Maze.InitialPos;
        }

        public void Finish(string s)
        {
            HandleFinish?.Invoke();
        }
    }
}