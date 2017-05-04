using System;
using System.Collections.Generic;
using System.Configuration;
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
using WpfApp.Single.PreGame;

namespace WpfApp.Single
{
    /// <summary>
    /// Interaction logic for PreSingleGameWindow.xaml
    /// </summary>
    public partial class PreSingleGameWindow : Window
    {
        public delegate void Start(string width, string height, string name);

        public event Start PressedOk;

        public delegate void finish();

        public event finish PressedCancel;

        private PreViewModel viewModel;

        public PreSingleGameWindow()
        {
            InitializeComponent();
            viewModel = new PreViewModel();
            viewModel.Notif += StartGame;
            this.DataContext = viewModel;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.PressedOk();
        }

        private void StartGame()
        {
            this.Close();
            PressedOk?.Invoke(viewModel.Width, viewModel.Height, viewModel.Name);
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}