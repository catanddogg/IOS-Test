using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace IllyaVirych.IOS.Views
{     
    [MvxRootPresentation]
    public class MainView : MvxViewController<MainViewModel>
    {
        private bool _firstTimePresented = true;

        public MainView()
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);            
            if (_firstTimePresented)
            {
                _firstTimePresented = false;                
                ViewModel.CurrentMainViewCommand.Execute(null);
            }
        }

    }
}