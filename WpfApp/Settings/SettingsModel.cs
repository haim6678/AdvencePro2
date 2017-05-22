using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Settings
{
    public class SettingsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsModel()
        {
            // initialize default fields by the app settings data.
            this.IP = SettingsManager.Instance.ReadSetting(SettingName.IP);
            this.Port = ushort.Parse(SettingsManager.Instance.ReadSetting(SettingName.Port));
            this.MazeWidth = uint.Parse(SettingsManager.Instance.ReadSetting(SettingName.Width));
            this.MazeHeight = uint.Parse(SettingsManager.Instance.ReadSetting(SettingName.Height));
            this.SearchAlgorithm = byte.Parse(SettingsManager.Instance.ReadSetting(SettingName.SearchAlgorithm));
        }

        /// <summary>
        /// the ip property
        /// </summary>
        private string ip;
        public string IP
        {
            get
            {
                return this.ip;
            }
            set
            {
                if (this.ip != value)
                {
                    this.ip = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IP"));
                }
            }
        }

        /// <summary>
        /// the port property
        /// </summary>
        private ushort port;
        public ushort Port
        {
            get
            {
                return this.port;
            }
            set
            {
                if (this.port != value)
                {
                    this.port = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Port"));
                }
            }
        }

        private uint width;
        public uint MazeWidth
        {
            get
            {
                return this.width;
            }
            set
            {
                if (this.width != value)
                {
                    this.width = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeWidth"));
                }
            }
        }

        private uint height;
        public uint MazeHeight
        {
            get
            {
                return this.height;
            }
            set
            {
                if (this.height != value)
                {
                    this.height = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeHeight"));
                }
            }
        }

        private byte searchAlg;
        public byte SearchAlgorithm
        {
            get
            {
                return this.searchAlg;
            }
            set
            {
                if (this.searchAlg != value)
                {
                    this.searchAlg = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchAlgorithm"));
                }
            }
        }

        public void SaveSettings()
        {
            SettingsManager.Instance.UpdateSetting(SettingName.IP, IP);
            SettingsManager.Instance.UpdateSetting(SettingName.Port, Port.ToString());
            SettingsManager.Instance.UpdateSetting(SettingName.Width, MazeWidth.ToString());
            SettingsManager.Instance.UpdateSetting(SettingName.Height, MazeHeight.ToString());
            SettingsManager.Instance.UpdateSetting(SettingName.SearchAlgorithm, SearchAlgorithm.ToString());
        }
    }
}