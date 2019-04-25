using Android.Gms.Ads;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Firebase.RemoteConfig;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.ViewModels;
using IllyaVirych.Droid.ViewAdapter;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class ListTaskView : BaseFragment<ListTaskViewModel>
    {
        #region Variables
        private MvxRecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;
        private InterstitialAd _interstitialAd;
        private bool _showAds;
        #endregion

        private IMvxInteraction<object> _interaction;
        public IMvxInteraction<object> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                _interaction = value;
                _interaction.Requested += OnInteractionRequested;
            }
        }

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            HasOptionsMenu = true;
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.MyApp);
            _layoutManager = new GridLayoutManager(this.Context, 2);
            _recyclerView.SetLayoutManager(_layoutManager);
            var recyclerAdapter = new TaskListAdapter((IMvxAndroidBindingContext)this.BindingContext);
            _recyclerView.Adapter = recyclerAdapter;
            var toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            ParentActivity.SetSupportActionBar(toolbar);
            ParentActivity.SupportActionBar.Title = "";
            ViewModel.ShowMenuViewModelCommand.Execute(null);

            _interstitialAd = new InterstitialAd(view.Context);
            _interstitialAd.AdUnitId = "ca-app-pub-3940256099942544/1033173712";
            _interstitialAd.LoadAd(new AdRequest.Builder().Build());

            var firebasePredictionsService = Mvx.Resolve<IFirebasePredictionsService>();
            _showAds = firebasePredictionsService.InitializeFirebasePredictions();

            var set = this.CreateBindingSet<ListTaskView, ListTaskViewModel>();
            set.Bind(this).For(v => v.Interaction).To(viewModel => viewModel.Interaction).OneWay();
            set.Apply();

            return view;
        }
        #endregion
        
        #region Override
        protected override int FragmentId => Resource.Layout.ListTaskView;
        #endregion

        private void OnInteractionRequested(object sender, MvxValueEventArgs<object> eventArgs)
        {
            if (_showAds)
            {
                if (_interstitialAd.IsLoaded)
                {
                    _interstitialAd.Show();
                }
            }
        }
    }
}