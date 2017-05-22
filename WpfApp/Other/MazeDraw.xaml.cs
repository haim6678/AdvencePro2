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

        private Rectangle[,] rectangles;
        private ImageBrush PlayerimageBrush;
        private ImageBrush ExitimageBrush;

        private SolidColorBrush blackBrush;
        private SolidColorBrush whitheBrush;
        private SolidColorBrush blueBrush;
        private double RecWidth;
        private double RecHeight; //todo relative
        private static Rectangle pos { get; set; } //todo will work with static
        private Rectangle GoalPos { get; set; }

        #endregion

        public MazeDraw()
        {
            PlayerimageBrush = new ImageBrush(new BitmapImage(
                new Uri(@"pack://application:,,,/WpfApp;component/Images/snoop-dogg.jpg")));

            ExitimageBrush = new ImageBrush(new BitmapImage(
                new Uri(@"pack://application:,,,/WpfApp;component/Images/images.jpg")));
            blackBrush = new SolidColorBrush(Colors.Black);
            whitheBrush = new SolidColorBrush(Colors.White);
            blueBrush = new SolidColorBrush(Colors.Blue);
            pos = new Rectangle();
            GoalPos = new Rectangle();
            InitializeComponent();
        }

        private void MazeDraw_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.MyCanvas.Height = Maze.Rows;
            this.MyCanvas.Width = Maze.Cols;
            RecWidth = this.MyCanvas.Width / Maze.Cols;
            RecHeight = this.MyCanvas.Height / Maze.Rows; //todo fixed size,or relative like this?
            rectangles = new Rectangle[Maze.Rows, Maze.Cols];

            for (int i = 0; i < Maze.Rows; i++)
            {
                for (int j = 0; j < Maze.Cols; j++)
                {
                    rectangles[i, j] = new Rectangle();
                    rectangles[i, j].Width = RecWidth;
                    rectangles[i, j].Height = RecHeight;

                    if (Maze[i, j] == CellType.Wall)
                    {
                        rectangles[i, j].Fill = blackBrush;
                    }
                    else
                    {
                        rectangles[i, j].Fill = whitheBrush;
                    }
                    this.MyCanvas.Children.Add(rectangles[i, j]);
                    double x = i * RecHeight;
                    double y = j * RecWidth;
                    Canvas.SetTop(rectangles[i, j], i * RecHeight);
                    Canvas.SetLeft(rectangles[i, j], j * RecWidth); //todo fix location according to size
                }
            }

            pos.Fill = PlayerimageBrush;
            pos.Width = RecWidth;
            pos.Height = RecHeight;
            MyCanvas.Children.Add(pos);
            Canvas.SetTop(pos, Maze.InitialPos.Row); //todo y first as top???
            Canvas.SetLeft(pos, Maze.InitialPos.Col);

            GoalPos.Fill = ExitimageBrush;
            GoalPos.Width = RecWidth;
            GoalPos.Height = RecHeight;
            MyCanvas.Children.Add(GoalPos);
            Canvas.SetTop(GoalPos, Maze.GoalPos.Row); //todo y first as top???
            Canvas.SetLeft(GoalPos, Maze.GoalPos.Col);
        }

        #region Properties
        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeDraw));



        public Position PlayerPos
        {
            get { return (Position)GetValue(PlayerPosProperty); }
            set { SetValue(PlayerPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerPosProperty =
            DependencyProperty.Register("PlayerPos", typeof(Position), typeof(MazeDraw), new UIPropertyMetadata(HandleNewPos));



        #endregion

        #region Positions

        private static void HandleNewPos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Position s = (Position)e.NewValue;
            MazeDraw m = d as MazeDraw;

            Canvas.SetTop(pos, s.Row);
            Canvas.SetLeft(pos, s.Col);
        }

        #endregion
    }
}