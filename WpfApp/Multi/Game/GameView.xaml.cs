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
    /// in charge of the multi view
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class GameView : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private GameVM vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public GameView(GameVM vm)
        {
            InitializeComponent();

            this.vm = vm;
            this.DataContext = vm;
            vm.GameStarted += GameStarted;
            vm.GameOver += GameOver;
        }

        /// <summary>
        /// Games the over.
        /// </summary>
        /// <param name="reason">The reason.</param>
        private void GameOver(string reason)
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Game Over: " + reason, "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                this.CloseWithoutEvent();
            });
        }

        /// <summary>
        /// Games the started.
        /// </summary>
        private void GameStarted()
        {
            this.Dispatcher.Invoke(() =>
            {
                waitLbl.Visibility = Visibility.Collapsed;
                MessageBox.Show("The game has started", "Game Started", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        /// <summary>
        /// Handles the key up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                vm.HandleMovement(e.Key);
            }
        }

        /// <summary>
        /// Closes the without event.
        /// </summary>
        private void CloseWithoutEvent()
        {
            this.Closing -= Window_Closing;
            this.Close();
            this.Closing += Window_Closing;
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
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
