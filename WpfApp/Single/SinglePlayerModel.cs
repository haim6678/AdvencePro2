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
                Maze = Maze.FromJSON(result.Data);
                this.Position = m.InitialPos;
            }
            else
            {
                //todo handle com failed
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

        private Position p;

        public Position Position
        {
            get { return p; }
            set
            {
                if (!this.p.Equals(value))
                {
                    this.p = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Position")); //todo check
                }
            }
        }

        private Maze m;

        public Maze Maze
        {
            get { return m; }
            set
            {
                this.m = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("")); //todo check
            }
        }

        #endregion

        #region moveLogic

        public void HandleMovement(Key k)
        {
            Position position = HandleMove(k);
            if ((position.Col != -1) && (position.Row != -1))
            {
                p = position;
            }
        }

        private Position HandleMove(Key k)
        {
            Position pos = new Position(-1, -1);

            switch (k)
            {
                case Key.Up:
                    if ((p.Row + 1 < m.Rows) && (m[p.Row + 1, p.Col] == CellType.Free))
                    {
                        pos = new Position(p.Row + 1, p.Col);
                    }
                    break;
                case Key.Down:
                    if ((p.Row - 1 >= 0) && (m[p.Row - 1, p.Col] == CellType.Free))
                    {
                        pos = new Position(p.Row - 1, p.Col);
                    }
                    break;
                case Key.Left:
                    if ((p.Col - 1 >= 0) && (m[p.Row, p.Col - 1] == CellType.Free))
                    {
                        pos = new Position(p.Row, p.Col - 1);
                    }
                    break;
                case Key.Right:
                    if ((p.Col + 1 < m.Cols) && (m[p.Row, p.Col + 1] == CellType.Free))
                    {
                        pos = new Position(p.Row, p.Col + 1);
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