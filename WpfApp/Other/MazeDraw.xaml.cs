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
        private ImageBrush imageBrush;

        private SolidColorBrush blackBrush;
        private SolidColorBrush whitheBrush;
        private SolidColorBrush blueBrush;
        private int RecWidth =;
        private int RecHeight =; //todo relative
        private Rectangle pos { get; set; }
        private Rectangle GoalPos { get; set; }

        public MazeDraw()
        {
            imageBrush = new ImageBrush(new BitmapImage(
                new Uri(@"pack://application:,,,/WpfApp;component/images/pic.png", UriKind.Relative)));
            blackBrush = new SolidColorBrush(Colors.Black);
            whitheBrush = new SolidColorBrush(Colors.White);
            blueBrush = new SolidColorBrush(Colors.Blue);
            pos = new Rectangle();
        }

        private void MazeDraw_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            int rows = int.Parse(GetValue(HeightProperty) as string);
            int cols = int.Parse(GetValue(WidthProperty) as string); //todo?


            rectangles = new Rectangle[rows, cols];

            for (int i = 0; i < rows; i++) //todo
            {
                for (int j = 0; j < cols; j++)
                {
                    rectangles[i, j] = new Rectangle();
                    rectangles[i, j].Width = Width;
                    rectangles[i, j].Height = Height;

                    if (maze[i, j] == CellType.Free) //todo
                    {
                        rectangles[i, j].Fill = whitheBrush; //todo??
                    }
                    else
                    {
                        rectangles[i, j].Fill = blackBrush;
                    }
                    MyCanvas.Children.Add(rectangles[i, j]); //todo?
                    Canvas.SetTop(rectangles[i, j], j * 3);
                    Canvas.SetLeft(rectangles[i, j], i * 3);
                }
            }


            pos.Fill = imageBrush;
            pos.Width = Width
            pos.Height = Height;
            MyCanvas.Children.Add(pos);

            Canvas.SetTop(pos, maze.InitialPos.Col); //todo col first as top???
            Canvas.SetLeft(pos, maze.InitialPos.Row);
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
            DependencyProperty.Register("VM_Width", typeof(string),
                typeof(MazeDraw), null);

        public string MazeWidth
        {
            get { return GetValue(MazeWidthProperty) as string; }
            set { SetValue(MazeWidthProperty, value); }
        }

        private static readonly DependencyProperty MazeHeightProperty =
            DependencyProperty.Register("VM_Heigt", typeof(string),
                typeof(MazeDraw), null);

        public string MazeHeight
        {
            get { return GetValue(MazeHeightProperty) as string; }
            set { SetValue(MazeHeightProperty, value); }
        }

        private static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("VM_Position", typeof(string),
                typeof(MazeDraw), new UIPropertyMetadata(HandleNewPos));

        public string PlayerPos
        {
            get { }
        }

        #endregion

        private static void HandleNewPos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string s = e.NewValue.ToString(); //todo check if work

            MazeDraw m = d as MazeDraw;
            //draw from start
            Canvas.SetTop(pos, p.Col);
            Canvas.SetLeft(rec, p.Row);
        }

        private void draw(MazeDraw m)
        {
        }
    }
}