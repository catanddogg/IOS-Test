using Firebase.RemoteConfig;
using Foundation;
using Google.MobileAds;
using IllyaVirych.Core;
using MvvmCross.Platforms.Ios.Core;
using System.Collections.Generic;
using System.Reflection;
using UIKit;

namespace IllyaVirych.IOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {   
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);
            Firebase.Core.App.Configure();
            MobileAds.Configure("ca-app-pub-5024755913411556~2042827060");
            //var remoteConfig = RemoteConfig.SharedInstance;
            //Dictionary<object, object> remoteConfigDefaults = new Dictionary<object, object>();
            //remoteConfigDefaults.Add("ads_enabled", true);
            //remoteConfig.SetDefaults(remoteConfigDefaults);
            return result;
        }
    }
}


