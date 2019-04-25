using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.RemoteConfig;
using Foundation;
using IllyaVirych.Core.Interface;
using UIKit;

namespace IllyaVirych.IOS.Services
{
    public class FirebasePredictionsService : IFirebasePredictionsService
    {
        public bool InitializeFirebasePredictions()
        {
            var remoteConfig = RemoteConfig.SharedInstance;
            Dictionary<object, object> remoteConfigDefaults = new Dictionary<object, object>();
            remoteConfigDefaults.Add("ads_enabled", true);
            remoteConfig.SetDefaults(remoteConfigDefaults);
            return remoteConfig["ads_enabled"].BoolValue;
        }
    }
}