using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using static IllyaVirych.Core.Services.LoginService;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class LoginView : BaseFragment<LoginViewModel>
    {      
        protected override int FragmentId => Resource.Layout.LoginView;
        private ImageButton _imageButton;              

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            _imageButton = view.FindViewById<ImageButton>(Resource.Id.image_button);
            
            _imageButton.Click += delegate
            {
                InstagramLogin();
            };            
            return view;
        }
        
        public void InstagramLogin()
        {
            ViewModel.LoginCommand.Execute();
            StartActivity(ViewModel.Authhenticator.GetUI(this.Context));            
        }          
    }
}