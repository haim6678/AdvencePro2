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


        public MultiViewModel(MultiModel mod)
        {
            model = mod;
            string[] arr = model.StartInfo.Split(' ');

            //case start
            if (arr.Length > 1)
            {
                wait = new Waitnig.Waiting();
                model.NotifyMessege += HandleStart;
                model.StartGame();
                wait.ShowDialog();
            }
            //case join
            else
            {
                model.StartGame();
                model.NotifyMessege += HandleJoin;
            }
        }


        public void Movement(Key k)
        {
            model.HandleMyMovement(k);
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

        #region back to menu

        public void BackToMenu()
        {
            model.BackToMenu();
        }

        #endregion
    }
}