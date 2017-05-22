using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    /// </summary>
    public partial class MazeConf : UserControl
    {

        public MazeConf()
        {
            InitializeComponent();
        }
        
        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeConf));



        public uint MazeRows
        {
            get { return (uint)GetValue(MazeRowsProperty); }
            set { SetValue(MazeRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeRows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeRowsProperty =
            DependencyProperty.Register("MazeRows", typeof(uint), typeof(MazeConf));



        public uint MazeColumns
        {
            get { return (uint)GetValue(MazeColumnsProperty); }
            set { SetValue(MazeColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeColumns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeColumnsProperty =
            DependencyProperty.Register("MazeColumns", typeof(uint), typeof(MazeConf));




    }
}
