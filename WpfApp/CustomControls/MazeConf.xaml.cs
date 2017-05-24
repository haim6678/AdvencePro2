using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.CustomControls
{
    /// <summary>
    /// Interaction logic for MazeConf.xaml
    /// in charge of the name rows cols user control
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeConf : UserControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeConf"/> class.
        /// </summary>
        public MazeConf()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze name property
        /// </summary>
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeConf));



        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public uint MazeRows
        {
            get { return (uint)GetValue(MazeRowsProperty); }
            set { SetValue(MazeRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeRows.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze rows property
        /// </summary>
        public static readonly DependencyProperty MazeRowsProperty =
            DependencyProperty.Register("MazeRows", typeof(string), typeof(MazeConf));



        /// <summary>
        /// Gets or sets the maze columns.
        /// </summary>
        /// <value>
        /// The maze columns.
        /// </value>
        public uint MazeColumns
        {
            get { return uint.Parse( GetValue(MazeColumnsProperty) as string); }
            set { SetValue(MazeColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeColumns.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze columns property
        /// </summary>
        public static readonly DependencyProperty MazeColumnsProperty =
            DependencyProperty.Register("MazeColumns", typeof(uint), typeof(MazeConf));


       
    }
}

