using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MazeLib;
using WpfApp.Single.Confirm;

namespace WpfApp.Multi
{
    public class MultiViewModel
    {
        public MultiModel model;
        public Maze m;
        private Waitnig.Waiting wait;

        public delegate void Notify();

        public event Notify NotifyFinish;


        public MultiViewModel(Communicator com, string s)
        {
            model = new MultiModel(com);
            model.NotifyFinish += HandelFinish;
            string[] arr = s.Split(' ');

            //case start
            if (arr.Length > 1)
            {
                wait = new Waitnig.Waiting();
                wait.ShowDialog();
                model.NotifyMessege += HandleStart;
                wait.ShowDialog();
                model.StartGame(s);
            }
            //case join
            else
            {
                model.JoinGame(s);
                model.NotifyMessege += HandleJoin;
            }
        }

        private void HandelFinish()
        {
            string s = model.FinishMessage;
            //todo display message
            NotifyFinish?.Invoke();
        }

        private void HandleStart()
        {
            wait.Close();
            model.NotifyMessege -= HandleStart;
            model.NotifyMessege += HandldeOtherMovement;
            CreateMaze(model.MessageData);
        }

        private void HandleJoin()
        {
            model.NotifyMessege -= HandleJoin;
            model.NotifyMessege += HandldeOtherMovement;

            CreateMaze(model.MessageData);
        }

        private void CreateMaze(string s)
        {
            //todo verify that we got the maze and not error
            //todo parse and display maze
        }

        private void HandldeOtherMovement()
        {
            //todo verify that we got a move and not error
            //todo parse and display move
            //todo check if other win
            
        }

        public void Movement(Key k)
        {
            

            string s;
            switch (k)
            {
                case Key.Up:
                    s = "up";
                    break;
                case Key.Down:
                    s = "down";
                    break;
                case Key.Left:
                    s = "left";
                    break;
                case Key.Right:
                    s = "right";
                    break;
                default:
                    s = "ignore";
                    break;
            }
            if (!s.Equals("ignore"))
            {
                model.HandleMyMovement(s);
                //todo display my move!!
                //todo check if i win
            }
        }

        #region back to menu

        public void BackToMenu()
        {
            ConfirmWindow confirm = new ConfirmWindow();
            confirm.NotifCancel += HandleCancel;
            confirm.NotifOk += HandleBack;
        }

        private void HandleBack()
        {
            NotifyFinish?.Invoke();
        }

        private void HandleCancel()
        {
        }

        #endregion
    }
}