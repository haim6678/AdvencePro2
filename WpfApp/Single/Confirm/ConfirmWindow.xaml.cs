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

namespace WpfApp.Single.Confirm
{
    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        public delegate void Notify();

        public event Notify NotifOk;
        public event Notify NotifCancel;
        public ConfirmWindow()
        {
            InitializeComponent();
        }

        private void Ok_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            NotifOk?.Invoke();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            NotifCancel?.Invoke();
        }
    }
}
