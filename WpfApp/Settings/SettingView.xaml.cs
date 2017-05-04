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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : Window
    {
        private SettingsViewModel viewModelSettings { get; set; }

        public delegate void Notify(string port, string ip);

        public event Notify notify;

        public delegate void NotifyFinish();

        public event NotifyFinish Finish;

        public SettingView(string por, string ip)
        {
            InitializeComponent();
            viewModelSettings = new SettingsViewModel();
            viewModelSettings.NotifyChange += UpdateChanced;
            viewModelSettings.NotifyFinish += FinishMethod;
            DataContext = viewModelSettings;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModelSettings.ClickedOk();
        }

        public void UpdateChanced()
        {
            notify?.Invoke(viewModelSettings.Port, viewModelSettings.Ip);
            this.Close();
        }

        private void FinishMethod()
        {
            this.Close();
            Finish?.Invoke();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Finish?.Invoke();
        }
    }
}