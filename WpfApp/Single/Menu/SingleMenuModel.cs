using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Settings;

namespace WpfApp.Single.Menu
{
    public class SingleMenuModel
    {
        public SingleMenuModel()
        {
            this.CommandToSend = null;
            this.MazeName = "rush_b";
            MazeColumns = uint.Parse(SettingsManager.Instance.ReadSetting(SettingName.Width));
            MazeRows = uint.Parse(SettingsManager.Instance.ReadSetting(SettingName.Height));
        }
        
        public string MazeName
        {
            get;set;
        }
        
        public uint MazeRows
        {
            get;set;
        }
        
        public uint MazeColumns
        {
            get;set;
        }
        
        public string CommandToSend
        {
            get;
            private set;
        }

        public void SetCommand()
        {
            CommandToSend = string.Format("generate {0} {1} {2}", MazeName, MazeRows, MazeColumns);
        }
    }
}
