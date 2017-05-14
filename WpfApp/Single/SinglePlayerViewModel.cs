using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MazeLib;
using WpfApp.Single.Confirm;

namespace WpfApp
{
    public class SinglePlayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SinglePlayerModel model;

        private ConfirmWindow confirm;

        public SinglePlayerViewModel(SinglePlayerModel mod)
        {
            model = mod;
            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        #region properties

        public string VM_Name
        {
            get { return model.Name; }
            set { model.Name = value; }
        }

        public string VM_Maze
        {
            get
            {
                
                return model.MazeString;
            }
            set { model.Maze = Maze.FromJSON(value); } //todo like this?
        }

        public string VM_Position
        {
            get { return model.Position.ToString(); }
            set
            {
                Position pos = new Position(int.Parse(value.Split(',')[0]), int.Parse(value.Split(',')[1]));
                model.Position = pos;
            }
        }

        public string VM_Width
        {
            get { return model.Width; }
            set { model.Width = value; }
        }

        public string VM_MazeString
        {
            get { return model.MazeString; }
            set { model.MazeString = value; }
        }

        public string VM_Height
        {
            get { return model.Height; }
            set { model.Height = value; }
        }

        #endregion

        #region start

        #endregion

        #region Movemonet

        public void NewLocation(Position p)
        {
            //todo draw the new location
        }

        public void HandleMovement(Key k)
        {
            model.HandleMovement(k);
        }

        #endregion

        #region solve

        public void Solve()
        {
            //model.Message += ReceivedSolveMaze;
            model.Solve();
        }

        private void ReceivedSolveMaze()
        {
            //todo handle display of solved maze
            //convert back to maze initialize maze and position     //todo <-
            //handle the gui creation  //todo <-
            //model.Message -= ReceivedSolveMaze;
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
            model.Restart();
        }

        private void HandleCancel()
        {
            confirm.Close();
        }

        #endregion

        #region back to menu

        public void BackToMenu()
        {
            BackToMenuCheck();
        }

        public void BackToMenuCheck()
        {
            confirm = new ConfirmWindow();
            confirm.NotifCancel += HandleCancel;
            confirm.NotifOk += HandleBack;
            confirm.ShowDialog();
        }

        private void HandleBack()
        {
            //todo display "you quit"

            model.BackToMenu();
        }

        #endregion
    }
}