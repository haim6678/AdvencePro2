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

namespace WpfApp.Multi
{
    /// <summary>
    /// Interaction logic for PreMultiWindow.xaml
    /// </summary>
    public partial class PreMultiWindow : Window
    {
        private PreMultiViewModel ViewModel { get; set; }

        public delegate void Notify(string s);

        public event Notify NotifyStart;


        public PreMultiWindow(Communicator com)
        {
            InitializeComponent();
            this.ViewModel = new PreMultiViewModel(com);

            this.DataContext = ViewModel;
        }

        private void Join_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NotifyStart += StartMethod;
            ViewModel.JoinClick();
        }

        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NotifyStart += StartMethod;
            ViewModel.Start();
        }


        private void StartMethod(string s)
        {
            this.Close();
            NotifyStart?.Invoke(s);
        }
    }
}