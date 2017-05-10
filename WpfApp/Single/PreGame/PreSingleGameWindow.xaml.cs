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
        private PreViewModel viewModel { get; set; }

        public PreSingleGameWindow(PreViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            this.DataContext = viewModel;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.PressedOk();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}