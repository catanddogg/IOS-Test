using IllyaVirych.Core.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Platforms.Wpf.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace IllyaVirych.WPF.Views
{
    /// <summary>
    /// Interaction logic for TestWPFView.xaml
    /// 
    /// </summary>
    public partial class TestWPFView : MvxWpfView
    {
        public TestWPFView()
        {
            InitializeComponent();
        }
    }
}
