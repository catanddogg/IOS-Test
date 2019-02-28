﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.Interface;

namespace IllyaVirych.Xamarin.UI.Droid.Services
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string messege)
        {
            Toast.MakeText(Application.Context, messege, ToastLength.Short).Show();
        }
    }
}