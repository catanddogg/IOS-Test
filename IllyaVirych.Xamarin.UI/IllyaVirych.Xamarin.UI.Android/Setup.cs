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
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace IllyaVirych.Xamarin.UI.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, App>
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