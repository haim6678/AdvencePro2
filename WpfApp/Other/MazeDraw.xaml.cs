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
    /// </summary>
    public partial class MazeDraw : UserControl
    {
        #region members

        internal Rectangle Player { get; private set; }

        #endregion

        public MazeDraw()
        {
            Player = new Rectangle();
            InitializeComponent();
        }

        public void Draw()
        {
            InitializeCanvas();
            double recWidth = MyCanvas.Width / Maze.Cols;
            double recHeight = MyCanvas.Height / Maze.Rows;

            DrawBoard(recWidth, recHeight);
            DrawGoal(recWidth, recHeight);
            DrawPlayer(recWidth, recHeight);
        }

        private void InitializeCanvas()
        {
            this.MyCanvas.Height = Maze.Rows;
            this.MyCanvas.Width = Maze.Cols;
        }

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
        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeDraw), new UIPropertyMetadata(HandleNewMaze));


        public Position PlayerPos
        {
            get { return (Position)GetValue(PlayerPosProperty); }
            set { SetValue(PlayerPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerPosProperty =
            DependencyProperty.Register("PlayerPos", typeof(Position), typeof(MazeDraw), new UIPropertyMetadata(HandleNewPos));
        
        #endregion

        private static void HandleNewMaze(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Maze m = e.NewValue as Maze;
            MazeDraw md = d as MazeDraw;
            
            md.Draw();
        }

        private static void HandleNewPos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Position s = (Position)e.NewValue;
            MazeDraw m = d as MazeDraw;

            Canvas.SetTop(m.Player, s.Row);
            Canvas.SetLeft(m.Player, s.Col);
        }
    }
}