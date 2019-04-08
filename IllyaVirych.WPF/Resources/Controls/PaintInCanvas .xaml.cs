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

namespace IllyaVirych.WPF.Resources.Controls
{
    /// <summary>
    /// Interaction logic for PaintInCanvas.xaml
    /// </summary>
    public partial class PaintInCanvas : UserControl
    {
        private Point currentPoint = new Point();
        //List<double> listPointsCanvas;
        private int i = 0;
        private double X1;
        private double X2;
        private double Y1;
        private double Y2;

        public PaintInCanvas()
        {
            InitializeComponent();
        }

        #region MouseClick
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(this);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {

                Line line = new Line();
                line.Stroke = SystemColors.WindowBrush;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;
                currentPoint = e.GetPosition(this);
                paintSurface.Children.Add(line);
            }
        }
        #endregion

        #region PaintLinesOutList
        public static readonly DependencyProperty ListPointsCanvasProperty = DependencyProperty.Register
          ("ListPointsCanvas", typeof(List<double>), typeof(PaintInCanvas));

        public List<double> ListPointsCanvas
        {
            get { return (List<double>)GetValue(ListPointsCanvasProperty); }
            set { SetValue(ListPointsCanvasProperty, value); }
        }

        public void SetUpPaintLinesOutList()
        {
            var myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            if (i != 10000)
            {
                X1 = ListPointsCanvas[i];
                myLine.X1 = X1;
                i = i + 1;
                X2 = ListPointsCanvas[i];
                myLine.X2 = X2;
                i = i + 1;
                Y1 = ListPointsCanvas[i];
                myLine.Y1 = Y1;
                i = i + 1;
                Y2 = ListPointsCanvas[i];
                myLine.Y2 = Y2;
                i = i + 1;
                myLine.HorizontalAlignment = HorizontalAlignment.Left;
                myLine.VerticalAlignment = VerticalAlignment.Center;
                myLine.StrokeThickness = 2;
                paintSurface.Children.Add(myLine);
            }
        }
        #endregion

        #region Polyline
        public void SetUpPolyline(double X, double Y)
        {
            polyline.Points.Add(new Point(X, Y));
        }
        #endregion

        #region PointFromEllipseWithPolyline
        public void SetUpPointFromEllipseWithPolyline(double X, double Y, SolidColorBrush solidColorBrush)
        {
            Ellipse ellipse = new Ellipse();
            Canvas.SetLeft(ellipse, X - 8);
            Canvas.SetTop(ellipse, Y - 8);
            ellipse.Height = 18;
            ellipse.Width = 18;
            ellipse.Fill = Brushes.Transparent;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = solidColorBrush;
            paintSurface.Children.Add(ellipse);
            Ellipse ellipse1 = new Ellipse();
            Canvas.SetLeft(ellipse1, X - 4);
            Canvas.SetTop(ellipse1, Y - 4);
            ellipse1.Height = 10;
            ellipse1.Width = 10;
            ellipse1.Fill = solidColorBrush;
            ellipse1.StrokeThickness = 1;
            ellipse1.Stroke = Brushes.Red;
            paintSurface.Children.Add(ellipse1);
            polyline.Points.Add(new Point(X, Y));
        }
        #endregion

        #region PathSemicircle
        public void SetUpPathSemicircle(SolidColorBrush solidColorBrush)
        {
            Path path = new Path();
            path.Stroke = solidColorBrush;
            path.StrokeThickness = 3;
            path.Data = Geometry.Parse("M 200,100 A 20 20, 0, 0 0, 200 350");
            paintSurface.Children.Add(path);
        }
        #endregion

        #region PointFromEllipse
        public void SetUpPointFromEllipse(double X, double Y, SolidColorBrush solidColorBrush)
        {
            Ellipse ellipse = new Ellipse();
            Canvas.SetLeft(ellipse, X-8);
            Canvas.SetTop(ellipse, Y-8);
            ellipse.Height = 18;
            ellipse.Width = 18;
            ellipse.Fill = Brushes.Transparent;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = solidColorBrush;
            paintSurface.Children.Add(ellipse);
            Ellipse ellipse1 = new Ellipse();
            Canvas.SetLeft(ellipse1, X - 4);
            Canvas.SetTop(ellipse1, Y - 4);
            ellipse1.Height = 10;
            ellipse1.Width = 10;
            ellipse1.Fill = solidColorBrush;
            ellipse1.StrokeThickness = 1;
            ellipse1.Stroke = Brushes.Black;
            paintSurface.Children.Add(ellipse1);
        }
        #endregion

        #region PointHelp
        public void SetUpPointHelp(double X, double Y, SolidColorBrush solidColorBrush, SolidColorBrush solidColorBrush1)
        {
            Ellipse ellipse = new Ellipse();
            Canvas.SetLeft(ellipse, X - 7);
            Canvas.SetTop(ellipse, Y - 7);
            ellipse.Height = 12;
            ellipse.Width = 12;
            ellipse.Fill = Brushes.Transparent;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = solidColorBrush;
            paintSurface.Children.Add(ellipse);
            Ellipse ellipse1 = new Ellipse();
            Canvas.SetLeft(ellipse1, X - 4);
            Canvas.SetTop(ellipse1, Y - 4);
            ellipse1.Height = 6;
            ellipse1.Width = 6;
            ellipse1.Fill = solidColorBrush;
            ellipse1.StrokeThickness = 1;
            ellipse1.Stroke = solidColorBrush1;
            paintSurface.Children.Add(ellipse1);
        }
        #endregion
    }

}

