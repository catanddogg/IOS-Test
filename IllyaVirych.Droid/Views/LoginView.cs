using Android.Gms.Ads;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.RemoteConfig;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        #region Variables
        private ImageButton _imageButton;
        #endregion

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _imageButton = view.FindViewById<ImageButton>(Resource.Id.image_button);
            _imageButton.Click += LoginInstagramClick;
            var firebasePredictionsService = Mvx.Resolve<IFirebasePredictionsService>();
            bool showAds = firebasePredictionsService.InitializeFirebasePredictions();
            var adView = view.FindViewById<AdView>(Resource.Id.adView);
            if (showAds)
            {
                AdRequest adRequest = new AdRequest.Builder().Build();
                adView.LoadAd(adRequest);
                adView.Visibility = ViewStates.Visible;
            }
            if (!showAds)
            {
                adView.Visibility = ViewStates.Gone;
            }
            return view;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _imageButton.Click -= LoginInstagramClick;
        }
        #endregion

        #region Override    
        protected override int FragmentId => Resource.Layout.LoginView;
        #endregion

        #region Methods
        public void LoginInstagramClick(object sender, EventArgs e)
        {
            InstagramLogin();
        }

        public void InstagramLogin()
        {
            ViewModel.LoginCommand.Execute();
            StartActivity(ViewModel.Authhenticator.GetUI(this.Context));
        }
        #endregion
    }
}