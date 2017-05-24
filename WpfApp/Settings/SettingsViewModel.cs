using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Settings
{
    /// <summary>
    /// the settings view model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SettingsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The model
        /// </summary>
        private SettingsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SettingsViewModel(SettingsModel model)
        {
            this.model = model;
            model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Model control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        /// <summary>
        /// Gets or sets the vm ip.
        /// </summary>
        /// <value>
        /// The vm ip.
        /// </value>
        public string VM_IP
        {
            get { return model.IP; }
            set { model.IP = value; }
        }


        /// <summary>
        /// Gets or sets the vm port.
        /// </summary>
        /// <value>
        /// The vm port.
        /// </value>
        public ushort VM_Port
        {
            get { return model.Port; }
            set { model.Port = value; }
        }


        /// <summary>
        /// Gets or sets the width of the vm maze.
        /// </summary>
        /// <value>
        /// The width of the vm maze.
        /// </value>
        public uint VM_MazeWidth
        {
            get { return model.MazeWidth; }
            set { model.MazeWidth = value; }
        }


        /// <summary>
        /// Gets or sets the height of the vm maze.
        /// </summary>
        /// <value>
        /// The height of the vm maze.
        /// </value>
        public uint VM_MazeHeight
        {
            get { return model.MazeHeight; }
            set { model.MazeHeight = value; }
        }

        /// <summary>
        /// Gets or sets the vm search algorithm.
        /// </summary>
        /// <value>
        /// The vm search algorithm.
        /// </value>
        public byte VM_SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set { model.SearchAlgorithm = value; }
        }

        /// <summary>
        /// Oks the pressed.
        /// </summary>
        public void OkPressed()
        {
            model.SaveSettings();
        }
    }
}