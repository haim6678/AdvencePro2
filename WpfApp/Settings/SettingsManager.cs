using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Settings
{
    /// <summary>
    /// the name enum
    /// </summary>
    public enum SettingName
    {
        /// <summary>
        /// The ip
        /// </summary>
        IP,
        /// <summary>
        /// The port
        /// </summary>
        Port,
        /// <summary>
        /// The width
        /// </summary>
        Width,
        /// <summary>
        /// The height
        /// </summary>
        Height,
        /// <summary>
        /// The search algorithm
        /// </summary>
        SearchAlgorithm
    }

    /// <summary>
    /// in charge of the settings managing
    /// </summary>
    public sealed class SettingsManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static readonly SettingsManager _instance = new SettingsManager();
        /// <summary>
        /// The setting names
        /// </summary>
        private Dictionary<SettingName, string> settingNames;
        /// <summary>
        /// Prevents a default instance of the <see cref="SettingsManager"/> class from being created.
        /// </summary>
        private SettingsManager()
        {
            settingNames = new Dictionary<SettingName, string>();
            settingNames.Add(SettingName.IP, "Ip");
            settingNames.Add(SettingName.Port, "PortNum");
            settingNames.Add(SettingName.Width, "Width");
            settingNames.Add(SettingName.Height, "Height");
            settingNames.Add(SettingName.SearchAlgorithm, "SearchAlgo");
        }

        /// <summary>
        /// Reads the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns></returns>
        public static string ReadSetting(SettingName setting)
        {
            return _instance.getSetting(setting);
        }
        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="value">The value.</param>
        public static void UpdateSetting(SettingName setting, string value)
        {
            _instance.setSetting(setting, value);
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns></returns>
        public string getSetting(SettingName setting)
        {
            return ConfigurationManager.AppSettings[settingNames[setting]];
        }
        /// <summary>
        /// Sets the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="value">The value.</param>
        public void setSetting(SettingName setting, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[settingNames[setting]].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
