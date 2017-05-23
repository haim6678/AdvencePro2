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

namespace WpfApp.Single
{
    /// <summary>
    /// Interaction logic for SinglePlayerView.xaml
    /// </summary>
    public partial class SinglePlayerView : Window
    {
        public SinglePlayerVM vm;

        public SinglePlayerView(SinglePlayerVM vm)
        {
            InitializeComponent();

            this.vm = vm;
            this.DataContext = this.vm;
            this.vm.GameOver += GameOver;
        }


        private void SinglePlayerView_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                vm.HandleMovement(e.Key);
            }
        }

        #region buttons

        private void Solve_OnClick(object sender, RoutedEventArgs e)
        {
            vm.Solve();
        }
        
        private void Resatrt_OnClick(object sender, RoutedEventArgs e)
        {
            vm.Restart();
        }

        private void Menu_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void GameOver(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (!this.IsActive)
                    return;
                MessageBox.Show(message);
                this.Close();
            });
        }

        #endregion
    }
}