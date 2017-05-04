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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Multi;
using WpfApp.Single;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string port;
        private string ip;


        public delegate void ComError();

        public event ComError CommunicationError;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            port = ConfigurationManager.AppSettings["PortNum"];
            ip = ConfigurationManager.AppSettings["Ip"];

            Menu m = new Menu(this);
            this.Title = "Welcome !";
            m.settingClick += HandleSetting;
            m.singleClick += HandleSingle;
            m.multiClick += HandleMulti;
            this.Content = m;
            this.Show();
        }

        #region settings

        public void HandleSetting()
        {
            SettingView settingView = new SettingView(port, ip);
            settingView.notify += HandleSettingChanged;
            settingView.Finish += FinishSetting;
            this.Hide();
            settingView.ShowDialog();
        }

        private void FinishSetting()
        {
            this.Show();
        }

        private void HandleSettingChanged(string por, string i)
        {
            this.port = por;
            this.ip = i;
            this.Show();
        }

        #endregion

        #region Single

        public void HandleSingle()
        {
            PreSingleGameWindow PreGame = new PreSingleGameWindow();
            PreGame.PressedOk += StartSingle; //todo not better to move this to the single view model??
            PreGame.Show();
        }

        public void StartSingle(string width, string height, string name)
        {
            SinglePlayerView singlePlayer = new SinglePlayerView(name, width, height);
            singlePlayer.Title = name;
            singlePlayer.GameEnded += FinishGame;
            this.Show();
        }

        private void FinishGame()
        {
        }

        #endregion

        #region multi

        private void HandleMulti()
        {
            MultiManager manager = new MultiManager(port, ip);
            manager.NotifyFinish += FinishGame; //todo todo like this for single player
        }
    }

    #endregion
}