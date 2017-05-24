using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp.Single.Menu
{
    /// <summary>
    ///  single player menu view model
    /// </summary>
    public class SingleMenuVM
    {
        /// <summary>
        /// The model
        /// </summary>
        private SingleMenuModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleMenuVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SingleMenuVM(SingleMenuModel model)
        {
            this.model = model;
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
            set { model.MazeName = value; }
        }

        /// <summary>
        /// Gets or sets the vm maze rows.
        /// </summary>
        /// <value>
        /// The vm maze rows.
        /// </value>
        public string VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                if (!IsTextAllowed(value))
                {
                    int i = 0;
                }
                model.MazeRows = value;
            }
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
        /// Sets the command.
        /// </summary>
        public void SetCommand()
        {
            model.SetCommand();
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
        /// Determines whether [is text allowed] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <c>true</c> if [is text allowed] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
    }
}