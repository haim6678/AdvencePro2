using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Settings
{
    /// <summary>
    /// the setting window model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SettingsModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsModel"/> class.
        /// </summary>
        public SettingsModel()
        {
            // initialize default fields by the app settings data.
            this.IP = SettingsManager.ReadSetting(SettingName.IP);
            this.Port = ushort.Parse(SettingsManager.ReadSetting(SettingName.Port));
            this.MazeWidth = uint.Parse(SettingsManager.ReadSetting(SettingName.Width));
            this.MazeHeight = uint.Parse(SettingsManager.ReadSetting(SettingName.Height));
            this.SearchAlgorithm = byte.Parse(SettingsManager.ReadSetting(SettingName.SearchAlgorithm));
        }

        /// <summary>
        /// the ip property
        /// </summary>
        private string ip;
        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        public string IP
        {
            get { return this.ip; }
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
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public ushort Port
        {
            get { return this.port; }
            set
            {
                if (this.port != value)
                {
                    this.port = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Port"));
                }
            }
        }

        /// <summary>
        /// The width
        /// </summary>
        private uint width;
        /// <summary>
        /// Gets or sets the width of the maze.
        /// </summary>
        /// <value>
        /// The width of the maze.
        /// </value>
        public uint MazeWidth
        {
            get { return this.width; }
            set
            {
                if (this.width != value)
                {
                    this.width = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeWidth"));
                }
            }
        }

        /// <summary>
        /// The height
        /// </summary>
        private uint height;
        /// <summary>
        /// Gets or sets the height of the maze.
        /// </summary>
        /// <value>
        /// The height of the maze.
        /// </value>
        public uint MazeHeight
        {
            get { return this.height; }
            set
            {
                if (this.height != value)
                {
                    this.height = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeHeight"));
                }
            }
        }

        /// <summary>
        /// The search alg
        /// </summary>
        private byte searchAlg;
        /// <summary>
        /// Gets or sets the search algorithm.
        /// </summary>
        /// <value>
        /// The search algorithm.
        /// </value>
        public byte SearchAlgorithm
        {
            get { return this.searchAlg; }
            set
            {
                if (this.searchAlg != value)
                {
                    this.searchAlg = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchAlgorithm"));
                }
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            SettingsManager.UpdateSetting(SettingName.IP, IP);
            SettingsManager.UpdateSetting(SettingName.Port, Port.ToString());
            SettingsManager.UpdateSetting(SettingName.Width, MazeWidth.ToString());
            SettingsManager.UpdateSetting(SettingName.Height, MazeHeight.ToString());
            SettingsManager.UpdateSetting(SettingName.SearchAlgorithm, SearchAlgorithm.ToString());
        }
    }
}