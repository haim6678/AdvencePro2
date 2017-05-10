using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Settings
{
    public enum Settings
    {
        IP,
        Port,
        Width,
        Height,
        SearchAlgorithm
    }

    public sealed class SettingsManager
    {
        private static readonly SettingsManager _instance = new SettingsManager();

        private Dictionary<Settings, string> settingNames;

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private SettingsManager()
        {
            settingNames = new Dictionary<Settings, string>();
            settingNames.Add(Settings.IP, "Ip");
            settingNames.Add(Settings.Port, "PortNum");
            settingNames.Add(Settings.Width, "Width");
            settingNames.Add(Settings.Height, "Height");
            settingNames.Add(Settings.SearchAlgorithm, "SearchAlgo");
        }


        public string ReadSetting(Settings setting)
        {
            return ConfigurationManager.AppSettings[settingNames[setting]];
        }
        public void UpdateSetting(Settings setting, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[settingNames[setting]].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
