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
    /// Interaction logic for MultiView.xaml
    /// </summary>
    public partial class MultiView : Window
    {
        private MultiViewModel viewModel;

        public delegate void Notify();

        public event Notify NotifyFinish;

        public MultiView(Communicator c, string s)
        {
            InitializeComponent();
            viewModel = new MultiViewModel(c, s);
            viewModel.NotifyFinish += HandleFinish;
        }

        private void HandleFinish()
        {
            this.Close();
            NotifyFinish?.Invoke();
        }

        private void Menu_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.BackToMenu();
        }


        private void MultiView_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                viewModel.Movement(e.Key);
            }
        }
    }
}