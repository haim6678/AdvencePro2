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
        private int InitialPosX { get; set; }
        private int InitialPosY { get; set; }
        private int ExitPosY { get; set; }
        private int ExitPosX { get; set; }
        private int rows { get; set; }
        private int cols { get; set; }

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
            rows = int.Parse(MazeHeight);
            cols = int.Parse(MazeWidth);
            GetSpecialPos();
            this.MyCanvas.Height = rows;
            this.MyCanvas.Width = cols;
            RecWidth = this.MyCanvas.Width / cols;
            RecHeight = this.MyCanvas.Height / rows; //todo fixed size,or relative like this?
            rectangles = new Rectangle[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rectangles[i, j] = new Rectangle();
                    rectangles[i, j].Width = RecWidth;
                    rectangles[i, j].Height = RecHeight;
                    string color;
                    if (Maze[i * cols + j].Equals('1'))
                    {
                        rectangles[i, j].Fill = blackBrush;
                        color = "black";
                    }
                    else
                    {
                        rectangles[i, j].Fill = whitheBrush;
                        color = "white";
                    }
                    this.MyCanvas.Children.Add(rectangles[i, j]);
                    double x = i * RecHeight;
                    double y = j * RecWidth;
                    Console.WriteLine("{0},{1},{2}", x, y, color);
                    Canvas.SetTop(rectangles[i, j], i * RecHeight);
                    Canvas.SetLeft(rectangles[i, j], j * RecWidth); //todo fix location according to size
                }
            }

            pos.Fill = PlayerimageBrush;
            pos.Width = RecWidth;
            pos.Height = RecHeight;
            MyCanvas.Children.Add(pos);
            Canvas.SetTop(pos, InitialPosX); //todo y first as top???
            Canvas.SetLeft(pos, InitialPosY);

            GoalPos.Fill = ExitimageBrush;
            GoalPos.Width = RecWidth;
            GoalPos.Height = RecHeight;
            MyCanvas.Children.Add(GoalPos);
            Canvas.SetTop(GoalPos, ExitPosX); //todo y first as top???
            Canvas.SetLeft(GoalPos, ExitPosY);
        }

        private void GetSpecialPos()
        {
            int size = rows * cols;
            for (int i = 0; i < size; i++)
            {
                if (Maze[i].Equals('#')) //todo exit it's *??
                {
                    ExitPosX = i / rows;
                    ExitPosY = i % rows;
                }
                if (Maze[i].Equals('*'))
                {
                    InitialPosX = i / rows;
                    InitialPosY = i % rows;
                }
            }
        }

        #region Properties

        private static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(string),
                typeof(MazeDraw), null);

        public string Maze
        {
            get { return GetValue(MazeProperty) as string; }
            set { SetValue(MazeProperty, value); }
        }

        private static readonly DependencyProperty MazeWidthProperty =
            DependencyProperty.Register("MazeWidth", typeof(string),
                typeof(MazeDraw), null);

        public string MazeWidth
        {
            get { return GetValue(MazeWidthProperty) as string; }
            set { SetValue(MazeWidthProperty, value); }
        }

        private static readonly DependencyProperty MazeHeightProperty =
            DependencyProperty.Register("MazeHeight", typeof(string),
                typeof(MazeDraw), null);

        public string MazeHeight
        {
            get { return GetValue(MazeHeightProperty) as string; }
            set { SetValue(MazeHeightProperty, value); }
        }

        private static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("PlayerPos", typeof(string),
                typeof(MazeDraw), new UIPropertyMetadata(HandleNewPos));

        public string PlayerPos
        {
            get { return GetValue(PositionProperty) as string; }
            set { SetValue(PositionProperty, value); }
        }

        #endregion

        private static void HandleNewPos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string s = e.NewValue.ToString(); //todo check if work
            MazeDraw m = d as MazeDraw;

            string[] arr = s.Split(',');
            int newX = (int) char.GetNumericValue((arr[0])[1]);
            int newY = (int) char.GetNumericValue((arr[1])[0]);
            Canvas.SetTop(pos, newX); //todo y first as top???
            Canvas.SetLeft(pos, newY);
        }
    }
}