using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Settings
{
    public enum SettingName
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

        private Dictionary<SettingName, string> settingNames;

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private SettingsManager()
        {
            settingNames = new Dictionary<SettingName, string>();
            settingNames.Add(SettingName.IP, "Ip");
            settingNames.Add(SettingName.Port, "PortNum");
            settingNames.Add(SettingName.Width, "Width");
            settingNames.Add(SettingName.Height, "Height");
            settingNames.Add(SettingName.SearchAlgorithm, "SearchAlgo");
        }


        public string ReadSetting(SettingName setting)
        {
            return ConfigurationManager.AppSettings[settingNames[setting]];
        }
        public void UpdateSetting(SettingName setting, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[settingNames[setting]].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
