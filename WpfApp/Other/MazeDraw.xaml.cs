using System;
using System.Collections.Generic;
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
using MazeLib;


namespace WpfApp.Other
{
    /// <summary>
    /// Interaction logic for MazeDraw.xaml
    /// the user control that draws the maze.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeDraw : UserControl
    {
        #region members

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        internal Rectangle Player { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeDraw"/> class.
        /// </summary>
        public MazeDraw()
        {
            Player = new Rectangle();
            InitializeComponent();
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            InitializeCanvas();
            double recWidth = MyCanvas.Width / Maze.Cols;
            double recHeight = MyCanvas.Height / Maze.Rows;

            DrawBoard(recWidth, recHeight);
            DrawGoal(recWidth, recHeight);
            DrawPlayer(recWidth, recHeight);
        }

        /// <summary>
        /// Initializes the canvas.
        /// </summary>
        private void InitializeCanvas()
        {
            this.MyCanvas.Height = Maze.Rows;
            this.MyCanvas.Width = Maze.Cols;
        }

        /// <summary>
        /// Draws the board.
        /// </summary>
        /// <param name="recWidth">Width of the record.</param>
        /// <param name="recHeight">Height of the record.</param>
        private void DrawBoard(double recWidth, double recHeight)
        {
            for (int i = 0; i < Maze.Rows; i++)
            {
                for (int j = 0; j < Maze.Cols; j++)
                {
                    Rectangle r = new Rectangle()
                    {
                        Width = recWidth,
                        Height = recHeight,
                        Fill = (Maze[i, j] == CellType.Wall) ? Brushes.Black : Brushes.White
                    };

                    this.MyCanvas.Children.Add(r);
                    double y = i * recHeight;
                    double x = j * recWidth;
                    Canvas.SetTop(r, y);
                    Canvas.SetLeft(r, x);
                }
            }
        }

        /// <summary>
        /// Draws the goal.
        /// </summary>
        /// <param name="recWidth">Width of the record.</param>
        /// <param name="recHeight">Height of the record.</param>
        private void DrawGoal(double recWidth, double recHeight)
        {
            ImageBrush goalBrush = new ImageBrush(new BitmapImage(
                new Uri(@"pack://application:,,,/WpfApp;component/Images/images.jpg")));
            Rectangle goal = new Rectangle()
            {
                Width = recWidth,
                Height = recHeight,
                Fill = goalBrush
            };
            MyCanvas.Children.Add(goal);
            Canvas.SetTop(goal, Maze.GoalPos.Row);
            Canvas.SetLeft(goal, Maze.GoalPos.Col);
        }

        /// <summary>
        /// Draws the player.
        /// </summary>
        /// <param name="recWidth">Width of the record.</param>
        /// <param name="recHeight">Height of the record.</param>
        private void DrawPlayer(double recWidth, double recHeight)
        {
            ImageBrush player = new ImageBrush(new BitmapImage(
                new Uri(@"pack://application:,,,/WpfApp;component/Images/snoop-dogg.jpg")));
            // player pos
            Player.Width = recWidth;
            Player.Height = recHeight;
            Player.Fill = player;

            MyCanvas.Children.Add(Player);
            Canvas.SetTop(Player, Maze.InitialPos.Row);
            Canvas.SetLeft(Player, Maze.InitialPos.Col);
        }

        #region Properties
        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        /// Using a DependencyProperty as the backing store for Maze.  
        ///This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze property
        /// </summary>
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeDraw), 
                new UIPropertyMetadata(HandleNewMaze));


        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public Position PlayerPos
        {
            get { return (Position)GetValue(PlayerPosProperty); }
            set { SetValue(PlayerPosProperty, value); }
        }

        /// Using a DependencyProperty as the backing store for PlayerPos.
        ///   This enables animation, styling, binding, etc...
        /// <summary>
        /// The player position property
        /// </summary>
        public static readonly DependencyProperty PlayerPosProperty =
            DependencyProperty.Register("PlayerPos", typeof(Position), typeof(MazeDraw), 
                new UIPropertyMetadata(HandleNewPos));

        #endregion

        /// <summary>
        /// Handles the new maze.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> 
        private static void HandleNewMaze(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Maze m = e.NewValue as Maze;
            MazeDraw md = d as MazeDraw;
            
            md.Draw();
        }

        /// <summary>
        /// Handles the new position.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/>
        private static void HandleNewPos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Position s = (Position)e.NewValue;
            MazeDraw m = d as MazeDraw;

            Canvas.SetTop(m.Player, s.Row);
            Canvas.SetLeft(m.Player, s.Col);
        }
    }
}