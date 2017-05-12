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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public delegate void userClick();

        public event userClick settingClick;
        public event userClick singleClick;
        public event userClick multiClick;


        public Menu(MainWindow window)
        {
            InitializeComponent();
            window.IsEnabled = true;
            this.Focus();
            this.DataContext = window;
        }


        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            settingClick?.Invoke();
        }

        private void Multi_OnClick(object sender, RoutedEventArgs e)
        {
            multiClick?.Invoke();
        }

        private void Single_OnClick(object sender, RoutedEventArgs e)
        {
            singleClick?.Invoke();
        }
    }
}