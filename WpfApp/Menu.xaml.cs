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
    /// the main menu view.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Menu : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the OnClick event of the Settings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsModel m = new SettingsModel();
            SettingsViewModel vm = new SettingsViewModel(m);
            SettingsView settingView = new SettingsView(vm);
            settingView.ShowDialog();
        }

        /// <summary>
        /// Handles the OnClick event of the Multi control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Multi_OnClick(object sender, RoutedEventArgs e)
        {
            string ip = SettingsManager.ReadSetting(SettingName.IP);
            int port = int.Parse(SettingsManager.ReadSetting(SettingName.Port));
            MultiManager starter = new MultiManager(ip, port);

            this.Hide();
            starter.Start();
            this.Show();
        }

        /// <summary>
        /// Handles the OnClick event of the Single control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Single_OnClick(object sender, RoutedEventArgs e)
        {
            string ip = SettingsManager.ReadSetting(SettingName.IP);
            int port = int.Parse(SettingsManager.ReadSetting(SettingName.Port));
            SingleManager manager = new SingleManager(ip, port);
            manager.Start();
        }
    }
}