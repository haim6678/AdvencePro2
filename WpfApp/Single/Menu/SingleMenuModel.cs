using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Settings;

namespace WpfApp.Single.Menu
{
    /// <summary>
    /// single player menu model
    /// </summary>
    public class SingleMenuModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleMenuModel"/> class.
        /// </summary>
        public SingleMenuModel()
        {
            this.CommandToSend = null;
            this.MazeName = "rush_b";
            MazeColumns = uint.Parse(SettingsManager.ReadSetting(SettingName.Width));
            MazeRows =(SettingsManager.ReadSetting(SettingName.Height));
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get;set;
        }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public string MazeRows
        {
            get;set;
        }

        /// <summary>
        /// Gets or sets the maze columns.
        /// </summary>
        /// <value>
        /// The maze columns.
        /// </value>
        public uint MazeColumns
        {
            get;set;
        }

        /// <summary>
        /// Gets the command to send.
        /// </summary>
        /// <value>
        /// The command to send.
        /// </value>
        public string CommandToSend
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the command.
        /// </summary>
        public void SetCommand()
        {
            CommandToSend = string.Format("generate {0} {1} {2}", MazeName, MazeRows, MazeColumns);
        }
    }
}
