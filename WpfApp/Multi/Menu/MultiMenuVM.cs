using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Multi.Menu
{
    /// <summary>
    /// the multi menu view mofel
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MultiMenuVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The model
        /// </summary>
        private MultiMenuModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiMenuVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiMenuVM(MultiMenuModel model)
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
        /// Gets the vm game list.
        /// </summary>
        /// <value>
        /// The vm game list.
        /// </value>
        public List<string> VM_GameList
        {
            get { return model.GameList; }
        }

        /// <summary>
        /// Gets or sets the name of the vm maze.
        /// </summary>
        /// <value>
        /// The name of the vm maze.
        /// </value>
        public string VM_MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value;}
        }

        /// <summary>
        /// Gets or sets the vm maze rows.
        /// </summary>
        /// <value>
        /// The vm maze rows.
        /// </value>
        public uint VM_MazeRows
        {
            get { return model.MazeRows; }
            set { model.MazeRows = value; }
        }

        /// <summary>
        /// Gets or sets the vm maze columns.
        /// </summary>
        /// <value>
        /// The vm maze columns.
        /// </value>
        public uint VM_MazeColumns
        {
            get { return model.MazeColumns; }
            set { model.MazeColumns = value; }
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <returns></returns>
        public string GetCommand()
        {
            return model.CommandToSend;
        }

        /// <summary>
        /// Refreshes the list.
        /// </summary>
        public void RefreshList()
        {
            model.UpdateAvailableGames();
        }

        /// <summary>
        /// Sets the start command.
        /// </summary>
        public void SetStartCommand()
        {
            model.SetStartCommand();
        }

        /// <summary>
        /// Sets the join command.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
        public void SetJoinCommand(string gameName)
        {
            model.SetJoinCommand(gameName);
        }
    }
}
