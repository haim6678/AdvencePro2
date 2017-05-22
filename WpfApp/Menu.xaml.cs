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
using WpfApp.Multi;
using WpfApp.Settings;
using WpfApp.Single;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }


        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsModel m = new SettingsModel();
            SettingsViewModel vm = new SettingsViewModel(m);
            SettingsView settingView = new SettingsView(vm);
            settingView.ShowDialog();
        }

        private void Multi_OnClick(object sender, RoutedEventArgs e)
        {
            string ip = SettingsManager.Instance.ReadSetting(SettingName.IP);
            int port = int.Parse(SettingsManager.Instance.ReadSetting(SettingName.Port));
            MultiManager starter = new MultiManager(ip, port);

            this.Hide();
            starter.Start();
            this.Show();
        }

        private void Single_OnClick(object sender, RoutedEventArgs e)
        {
            string ip = SettingsManager.Instance.ReadSetting(SettingName.IP);
            int port = int.Parse(SettingsManager.Instance.ReadSetting(SettingName.Port));
            SingleManager manager = new SingleManager(ip, port);
            manager.Start();
        }
    }
}