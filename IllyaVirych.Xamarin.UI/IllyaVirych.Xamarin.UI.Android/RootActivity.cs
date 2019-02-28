using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using OxyPlot.Xamarin.Forms.Platform.Android;

namespace IllyaVirych.Xamarin.UI.Droid
{
    [Activity(Label = "IllyaVirych.Xamarin.UI",
         Icon = "@mipmap/icon",
         Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class RootActivity : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(bundle);
                PlotViewRenderer.Init();
                //global::Xamarin.Forms.Forms.Init(this, bundle);
                //LoadApplication(new App());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}