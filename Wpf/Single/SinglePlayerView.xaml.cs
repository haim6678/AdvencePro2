using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf.Single
{
    /// <summary>
    /// Interaction logic for SinglePlayerView.xaml
    /// </summary>
    public partial class SinglePlayerView : Window
    {
        public delegate void NotifyFinish();
        public event NotifyFinish GameEnded;
        public SinglePlayerViewModel ViewModel;

        public SinglePlayerView(string name,string width,string height)
        {
            InitializeComponent();
            this.ViewModel = new SinglePlayerViewModel();
            ViewModel.NotifFinish += Finish;
            this.DataContext = ViewModel;
            ViewModel.StartSingle(name,width,height);
        }

        private void Finish()
        {
            this.Close();
            GameEnded?.Invoke();
        }

        private void SinglePlayerView_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                ViewModel.HandleMovement(e.Key);
            }
        }

        #region buttons

        
        private void Solve_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Solve();
        }


        private void Resatrt_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Restart();
        }

        private void Menu_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.BackToMenu();
        }
        #endregion
    }
}