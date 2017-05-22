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
    /// </summary>
    public partial class MultiMenu : Window
    {
        private MultiMenuVM vm;

        public MultiMenu(MultiMenuVM vm)
        {
            this.vm = vm;
            this.DataContext = vm;
            InitializeComponent();
        }

        public bool? ShowDialog(out string command)
        {
            bool? dr = ShowDialog();
            command = null;
            if (dr.HasValue)
                command = vm.GetCommand();
            return dr;
        }

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

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.SetStartCommand();
            this.Close();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.RefreshList();
        }
    }
}
