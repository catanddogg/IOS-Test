using IllyaVirych.Core.Messenger;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Plugin.Messenger;
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

namespace IllyaVirych.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MvxWindow
    {
        private IMvxMessenger _message;
        public MainWindow()
        {
            InitializeComponent();
            _message = Mvx.Resolve<IMvxMessenger>();

        }

        private void WPFClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseEventTestMessenger message = new CloseEventTestMessenger(this);
            _message.Publish(message);
        }
    }
}
