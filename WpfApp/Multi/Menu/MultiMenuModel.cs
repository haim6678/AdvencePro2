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
    /// <summary>
    /// the multi menu model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MultiMenuModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiMenuModel"/> class.
        /// </summary>
        public MultiMenuModel()
        {
            CommandToSend = null;
            UpdateAvailableGames();
            // load default values
            MazeName = "I_like_turtles";
            MazeColumns = uint.Parse(SettingsManager.ReadSetting(SettingName.Width));
            MazeRows = uint.Parse(SettingsManager.ReadSetting(SettingName.Height));
        }

        /// <summary>
        /// Updates the available games.
        /// </summary>
        public void UpdateAvailableGames()
        {
            List<string> availableGames = null;
            Communicator c = new Communicator(
                SettingsManager.ReadSetting(SettingName.IP), 
                int.Parse(SettingsManager.ReadSetting(SettingName.Port)));

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

            this.GameList = availableGames;
        }

        /// <summary>
        /// The game list
        /// </summary>
        private List<string> gameList;
        /// <summary>
        /// Gets the game list.
        /// </summary>
        /// <value>
        /// The game list.
        /// </value>
        public List<string> GameList
        {
            get { return gameList; }
            private set
            {
                this.gameList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GameList"));
            }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public uint MazeRows
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the maze columns.
        /// </summary>
        /// <value>
        /// The maze columns.
        /// </value>
        public uint MazeColumns
        {
            get; set;
        }


        /// <summary>
        /// Gets the command to send.
        /// </summary>
        /// <value>
        /// The command to send.
        /// </value>
        public string CommandToSend
        {
            get; private set;
        }

        /// <summary>
        /// Sets the start command.
        /// </summary>
        public void SetStartCommand()
        {
            CommandToSend = string.Format("start {0} {1} {2}", MazeName, MazeRows, MazeColumns);
        }

        /// <summary>
        /// Sets the join command.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
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
