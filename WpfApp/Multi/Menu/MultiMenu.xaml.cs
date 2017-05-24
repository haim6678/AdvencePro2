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

namespace WpfApp.Multi.Menu
{
    /// <summary>
    /// Interaction logic for MultiMenu.xaml
    /// the menu view
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiMenu : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private MultiMenuVM vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiMenu"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public MultiMenu(MultiMenuVM vm)
        {
            this.vm = vm;
            this.DataContext = vm;
            InitializeComponent();
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public bool? ShowDialog(out string command)
        {
            bool? dr = ShowDialog();
            command = null;
            if (dr.HasValue)
                command = vm.GetCommand();
            return dr;
        }

        /// <summary>
        /// Handles the Click event of the joinBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void joinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedIndex < 0)
            {
                MessageBox.Show("You must choose a game to join!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            vm.SetJoinCommand(ListBox.SelectedItem.ToString());
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the startBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.SetStartCommand();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the refreshBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.RefreshList();
        }
    }
}
