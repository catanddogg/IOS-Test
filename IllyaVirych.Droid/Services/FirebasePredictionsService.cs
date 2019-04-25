using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.RemoteConfig;
using IllyaVirych.Core.Interface;

namespace IllyaVirych.Droid.Services
{
    public class FirebasePredictionsService : IFirebasePredictionsService
    {
        public bool InitializeFirebasePredictions()
        {
            var firebaseRemoteConfig = FirebaseRemoteConfig.Instance;
            Dictionary<string, Java.Lang.Object> remoteConfigDefaults = new Dictionary<string, Java.Lang.Object>();
            remoteConfigDefaults.Add("ads_enabled", "true");
            firebaseRemoteConfig.SetDefaults(remoteConfigDefaults);
            return firebaseRemoteConfig.GetBoolean("ads_enabled");
        }
    }
}