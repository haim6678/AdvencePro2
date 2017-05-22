using Newtonsoft.Json;
using SharedData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Communication;
using WpfApp.Settings;

namespace WpfApp.Multi.Menu
{
    public class MultiMenuModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string ip;
        private int port;

        public MultiMenuModel(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            CommandToSend = null;
            UpdateAvailableGames();
            // load default values
            MazeName = "I_like_turtles";
            MazeColumns = uint.Parse(SettingsManager.Instance.ReadSetting(SettingName.Width));
            MazeRows = uint.Parse(SettingsManager.Instance.ReadSetting(SettingName.Height));
        }

        public void UpdateAvailableGames()
        {
            List<string> availableGames = null;
            Communicator c = new Communicator(ip, port);
            c.SendMessage("list");
            string response = c.ReadMessage();

            Message msg = Message.FromJSON(response);
            if (msg.MessageType == MessageType.CommandResult)
            {
                CommandResult res = CommandResult.FromJSON(msg.Data);
                if (res.Success && res.Command == Command.List)
                {
                    availableGames = JsonConvert.DeserializeObject<List<string>>(res.Data);
                }
            }

            // if something happened, just initialize the list so it wont be null
            if (availableGames == null)
            {
                availableGames = new List<string>();
            }

            // dispose the communicator
            c.Dispose();

            this.GameList = new ObservableCollection<string>(availableGames);
        }

        private ObservableCollection<string> gameList;
        public ObservableCollection<string> GameList
        {
            get { return gameList; }
            private set
            {
                this.gameList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GameList"));
            }
        }

        public string MazeName
        {
            get; set;
        }

        public uint MazeRows
        {
            get; set;
        }

        public uint MazeColumns
        {
            get; set;
        }


        public string CommandToSend
        {
            get; private set;
        }

        public void SetStartCommand()
        {
            CommandToSend = string.Format("start {0} {1} {2}", MazeName, MazeRows, MazeColumns);
        }

        public void SetJoinCommand(string gameName)
        {
            UpdateAvailableGames();
            if (GameList.Contains(gameName))
            {
                CommandToSend = "join " + gameName;
            }
        }
    }
}
