using System;
using Android.App;
using Android.Runtime;
using IllyaVirych.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IllyaVirych.Droid
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication()
        {
        }
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            Firebase.FirebaseApp.InitializeApp(this);
            base.OnCreate();
        }
    }
}