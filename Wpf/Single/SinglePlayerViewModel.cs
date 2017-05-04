using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MazeLib;
using Wpf.Single.Confirm;

namespace Wpf
{
    public class SinglePlayerViewModel
    {
        private SinglePlayerModel model;

        public delegate void Notify();
        public event Notify NotifFinish;

        private Position p { get; set; }
        private Maze m { get; set; } 
        public string Name { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        public SinglePlayerViewModel()
        {
            model = new SinglePlayerModel();

            model.HandleFinish += HandleFinish;
        }

        #region start
        
        public void StartSingle(string name, string widt, string heigh)
        {
            this.Name = name;
            model.Message += CreateMaze;
            model.start(name, widt, heigh);
        }

        private void CreateMaze()
        {
            //todo handle creation with data binding
            model.Message -= CreateMaze;
            SharedData.Message msg;
            msg = SharedData.Message.FromJSON(model.MassageData);
        }

        #endregion

        #region Movemonet

        public void NewLocation(Position p)
        {
            //todo draw the new location
        }

        public void HandleMovement(Key k)
        {
            //todo call the model or do this here??

            Position pos = HandleMove(k);
            if ((pos.Col != -1) && (pos.Row != -1))
            {
                p = pos;
                //todo check here if win

                //todo draw new loc
            }
        }

        public Position HandleMove(Key k)
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

        #region solve

        public void Solve()
        {
            model.Message += ReceivedSolveMaze;
            model.Solve(Name);
        }

        private void ReceivedSolveMaze()
        {
            //todo handle display of solved maze
            //convert back to maze initialize maze and position     //todo <-
            //handle the gui creation  //todo <-
            model.Message -= ReceivedSolveMaze;
        }

        #endregion

        #region resatrt
    
        public void Restart()
        {
            ConfirmWindow confirm = new ConfirmWindow();
            confirm.NotifOk += HandleRestart;
            confirm.NotifCancel += HandleCancel;
            confirm.ShowDialog();
        }

        private void HandleRestart()
        {
            p = m.InitialPos;
            //todo display new location
        }

        private void HandleCancel()
        {
        }

        #endregion

        #region back to menu

        public void BackToMenu()
        {
            BackToMenuCheck();
        }

        public void BackToMenuCheck()
        {
            ConfirmWindow confirm = new ConfirmWindow();
            confirm.NotifCancel += HandleCancel;
            confirm.NotifOk += HandleBack;
        }

        private void HandleBack()
        {
            //todo display "you quit"

            NotifFinish?.Invoke();
        }
        #endregion

        public void HandleFinish(string s)
        {
            switch (s)
            {
                case "you quit":
                    //todo display
                    break;
                case "you win":
                    //todo display
                    break;
                default:
                    break;
            }
            NotifFinish?.Invoke();
        }
    }
}