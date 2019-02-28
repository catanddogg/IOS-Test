using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;
using UIKit;

namespace IllyaVirych.Xamarin.UI.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, App>
    {
        protected override IMvxApplication CreateApp()
        {
            CreatableTypes()
              .EndingWith("Service")
              .AsInterfaces()
              .RegisterAsLazySingleton();
            return base.CreateApp();
        }
    }
}