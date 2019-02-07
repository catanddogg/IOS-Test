using Foundation;
using IllyaVirych.Core;
using MvvmCross.Platforms.Ios.Core;
using System.Reflection;
using UIKit;

namespace IllyaVirych.IOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        //public override UIWindow Window
        //{
        //    get;
        //    set;
        //}

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {

            var result = base.FinishedLaunching(application, launchOptions);
            return result;
        }
    }
}


