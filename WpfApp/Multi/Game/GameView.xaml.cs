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

namespace WpfApp.Multi.Game
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private GameVM vm;

        public GameView(GameVM vm)
        {
            InitializeComponent();

            this.vm = vm;
            this.DataContext = vm;
            vm.GameStarted += GameStarted;
            vm.GameOver += GameOver;
        }

        private void GameOver(string reason)
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Game Over: " + reason, "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                this.CloseWithoutEvent();
            });
        }

        private void GameStarted()
        {
            MessageBox.Show("The game has started", "Game Started", MessageBoxButton.OK, MessageBoxImage.Information);
            // TODO : unhide something that hides the screen.
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                vm.HandleMovement(e.Key);
            }
        }

        private void CloseWithoutEvent()
        {
            this.Closing -= Window_Closing;
            this.Close();
            this.Closing += Window_Closing;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to close the game?", "Are You Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                vm.CloseGame();
            }
            else
            {
                e.Cancel = true;
            }
            
        }
    }
}
