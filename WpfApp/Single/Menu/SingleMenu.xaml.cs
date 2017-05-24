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

namespace WpfApp.Single.Menu
{
    /// <summary>
    /// Interaction logic for SingleMenu.xaml
    /// single player view
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SingleMenu : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private SingleMenuVM vm;
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleMenu"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public SingleMenu(SingleMenuVM vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.DataContext = this.vm;
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
        /// Handles the Click event of the startBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.SetCommand();
            this.Close();
        }
    }
}
