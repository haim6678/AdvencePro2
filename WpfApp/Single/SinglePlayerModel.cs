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

namespace WpfApp
{
    public class SinglePlayerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void GameEnded();

        public event GameEnded HandleFinish;
        public string FinishMassage;


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
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(portNumber), int.Parse(ipNumber));
            TcpClient client = new TcpClient();
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(s);
            s = reader.ReadString();
            //todo check how to handle data
            client.Close();
        }

        public void start(string name, string width, string height)
        {
            Communicate(name + " " + width + " " + height);
        }


        public void Solve(string s)
        {
            string str = ConfigurationManager.AppSettings["SearchAlgo"];
            Communicate("solve " + s + " " + str);
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("")); //todo check
                }
            }
        }

        private Maze m;

        public Maze Maze
        {
            get { return m; }
            set
            {
                if (!m.Equals(value))
                {
                    this.m = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("")); //todo check
                }
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
    }
}