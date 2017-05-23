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
        private SettingsManager()
        {
            settingNames = new Dictionary<SettingName, string>();
            settingNames.Add(SettingName.IP, "Ip");
            settingNames.Add(SettingName.Port, "PortNum");
            settingNames.Add(SettingName.Width, "Width");
            settingNames.Add(SettingName.Height, "Height");
            settingNames.Add(SettingName.SearchAlgorithm, "SearchAlgo");
        }

        public static string ReadSetting(SettingName setting)
        {
            return _instance.getSetting(setting);
        }
        public static void UpdateSetting(SettingName setting, string value)
        {
            _instance.setSetting(setting, value);
        }

        public string getSetting(SettingName setting)
        {
            return ConfigurationManager.AppSettings[settingNames[setting]];
        }
        public void setSetting(SettingName setting, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[settingNames[setting]].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
