using MvvmCross.Platforms.Wpf.Views;
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
using System.Windows.Shapes;

namespace IllyaVirych.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : MvxWpfView
    {
        public List<double> listPointsCanvas;

        public LoginView()
        {
            InitializeComponent();
            SetUpPictureInCanvas();
        }

        private void SetUpPictureInCanvas()
        {
            //H
            PaintInCanvas.SetUpPointHelp(550, 10, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(550, 23, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(550, 36, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(550, 49, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(550, 62, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(563, 36, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(576, 10, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(576, 23, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(576, 36, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(576, 49, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(576, 62, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            //E
            PaintInCanvas.SetUpPointHelp(596, 10, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(596, 23, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(596, 36, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(596, 49, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(596, 62, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));            
            PaintInCanvas.SetUpPointHelp(609, 10, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(622, 10, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(609, 36, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(622, 36, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(609, 62, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(622, 62, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            //L
            PaintInCanvas.SetUpPointHelp(642, 10, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(642, 23, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(642, 36, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(642, 49, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(642, 62, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(655, 62, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(668, 62, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(668, 49, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            //P
            PaintInCanvas.SetUpPointHelp(688, 10, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(688, 23, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(688, 36, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(688, 49, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(688, 62, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(701, 10, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(714, 10, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(714, 23, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(714, 36, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(701, 36, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            //!
            PaintInCanvas.SetUpPointHelp(734, 10, new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(734, 23, new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Yellow));
            PaintInCanvas.SetUpPointHelp(734, 36, new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Black));
            PaintInCanvas.SetUpPointHelp(734, 62, new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Yellow));
            //
            PaintInCanvas.SetUpPointFromEllipse(600, 100, new SolidColorBrush(Colors.White));
            PaintInCanvas.SetUpPointFromEllipse(590, 150, new SolidColorBrush(Colors.White));
            PaintInCanvas.SetUpPointFromEllipse(650, 140, new SolidColorBrush(Colors.White));
            PaintInCanvas.SetUpPathSemicircle(new SolidColorBrush(Colors.Red));
            PaintInCanvas.SetUpPointFromEllipseWithPolyline(200, 100, new SolidColorBrush(Colors.Red));
            PaintInCanvas.SetUpPointFromEllipseWithPolyline(300, 100, new SolidColorBrush(Colors.Red));
            PaintInCanvas.SetUpPolyline(150, 225);
            PaintInCanvas.SetUpPolyline(150, 230);
            PaintInCanvas.SetUpPolyline(150, 220);
            PaintInCanvas.SetUpPolyline(150, 225);
            PaintInCanvas.SetUpPointFromEllipseWithPolyline(300, 350, new SolidColorBrush(Colors.Red));
            PaintInCanvas.SetUpPolyline(300, 120);
            PaintInCanvas.SetUpPolyline(300, 350);
            PaintInCanvas.SetUpPointFromEllipseWithPolyline(200, 350, new SolidColorBrush(Colors.Red));
            PaintInCanvas.SetUpPolyline(550, 350);
            PaintInCanvas.SetUpPolyline(650, 300);
            PaintInCanvas.SetUpPointFromEllipse(550, 320, new SolidColorBrush(Colors.Red));
            PaintInCanvas.SetUpPointFromEllipse(550, 370, new SolidColorBrush(Colors.Red));
            PaintInCanvas.SetUpPointFromEllipse(620, 340, new SolidColorBrush(Colors.Red));
        }
    }
}
