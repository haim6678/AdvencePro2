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
    ///  single player  view.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerView : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        public SinglePlayerVM vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerView"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public SinglePlayerView(SinglePlayerVM vm)
        {
            InitializeComponent();

            this.vm = vm;
            this.DataContext = this.vm;
            this.vm.GameOver += GameOver;
        }


        /// <summary>
        /// Handles the OnKeyUp event of the SinglePlayerView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void SinglePlayerView_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                vm.HandleMovement(e.Key);
            }
        }

        #region buttons

        /// <summary>
        /// Handles the OnClick event of the Solve control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Solve_OnClick(object sender, RoutedEventArgs e)
        {
            vm.Solve();
        }

        /// <summary>
        /// Handles the OnClick event of the Resatrt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Resatrt_OnClick(object sender, RoutedEventArgs e)
        {
            vm.Restart();
        }

        /// <summary>
        /// Handles the OnClick event of the Menu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Menu_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Games the over.
        /// </summary>
        /// <param name="message">The message.</param>
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