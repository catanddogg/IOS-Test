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
    /// Interaction logic for PointFromEllipse.xaml
    /// </summary>
    public partial class PointFromEllipse : UserControl
    {
        private Point currentPoint = new Point();

        public PointFromEllipse()
        {
            InitializeComponent();
            SetUpPointFromEllipse();
        }

        public static readonly DependencyProperty XPointFromEllipseProperty = DependencyProperty.Register
            ("X", typeof(double), typeof(PointFromEllipse));

        public double X
        {   get { return (double)GetValue(XPointFromEllipseProperty); }
            set { SetValue(XPointFromEllipseProperty, value); }       }


        public static readonly DependencyProperty YPointFromEllipseProperty = DependencyProperty.Register
            ("Y", typeof(double), typeof(PointFromEllipse));

        public double Y
        {   get { return (double)GetValue(YPointFromEllipseProperty); }
            set { SetValue(YPointFromEllipseProperty, value); }      }

        private void SetUpPointFromEllipse()
        {
            Ellipse ellipse = new Ellipse();
            Canvas.SetLeft(ellipse, X - 8);
            Canvas.SetTop(ellipse, Y - 8);
            ellipse.Height = 11;
            ellipse.Width = 11;
            ellipse.Fill = Brushes.Transparent;
            ellipse.StrokeThickness = 1;
            ellipse.Stroke = Brushes.White;
            paintSurface.Children.Add(ellipse);
            Ellipse ellipse1 = new Ellipse();
            Canvas.SetLeft(ellipse1, X - 4);
            Canvas.SetTop(ellipse1, Y - 4);
            ellipse1.Height = 3;
            ellipse1.Width = 3;
            ellipse1.Fill = Brushes.White;
            ellipse1.StrokeThickness = 3;
            ellipse1.Stroke = Brushes.White;
            paintSurface.Children.Add(ellipse1);
        }
    }
}
