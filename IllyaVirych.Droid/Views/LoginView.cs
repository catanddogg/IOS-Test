using Android.Gms.Ads;
using Android.OS;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.ViewModels;
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
        //private InterstitialAd _interstitialAd;
        #endregion

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _imageButton = view.FindViewById<ImageButton>(Resource.Id.image_button);
            _imageButton.Click += LoginInstagramClick;

            //_interstitialAd = new InterstitialAd(view.Context);
            //_interstitialAd.AdUnitId = "ca-app-pub-3940256099942544/1033173712";
            //_interstitialAd.LoadAd(new AdRequest.Builder().Build());

            var adView = view.FindViewById<AdView>(Resource.Id.adView);
            AdRequest adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);

            //Firebase Predictions
            //var firebaseRemoteConfig = FirebaseRemoteConfig.Instance;
            //Dictionary<string, Java.Lang.Object> remoteConfigDefaults =  new Dictionary<string, Java.Lang.Object>();
            //remoteConfigDefaults.Add("ads_enabled", "true");
            //firebaseRemoteConfig.SetDefaults(remoteConfigDefaults);
            //
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
            //if (_interstitialAd.IsLoaded)
            //{
            //    _interstitialAd.Show();
            //}
        }

        public void InstagramLogin()
        {
            ViewModel.LoginCommand.Execute();
            StartActivity(ViewModel.Authhenticator.GetUI(this.Context));
        }
        #endregion
    }
}