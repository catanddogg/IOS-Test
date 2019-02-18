using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.ViewModels;
using IllyaVirych.Droid.ViewAdapter;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using Xamarin.Essentials;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class ListTaskView : BaseFragment<ListTaskViewModel>
    {
        MvxRecyclerView _recyclerView;
        RecyclerView.LayoutManager _layoutManager;
        protected override int FragmentId => Resource.Layout.ListTaskView;        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            HasOptionsMenu = true;
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.RecyclerView);
            _layoutManager = new GridLayoutManager(this.Context, 2);
            _recyclerView.SetLayoutManager(_layoutManager);
            var recyclerAdapter = new TaskListAdapter((IMvxAndroidBindingContext)this.BindingContext);
            _recyclerView.Adapter = recyclerAdapter;
            var toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            ParentActivity.SetSupportActionBar(toolbar);
            ParentActivity.SupportActionBar.Title = "";
            ViewModel.ShowMenuViewModelCommand.Execute(null);

            var buttonddTaskSave = view.FindViewById<ImageButton>(Resource.Id.ButtonAddTaskToolbar);
            buttonddTaskSave.Click += ButtonAddTaskSaveClick;

            return view;
        }

        private void ButtonAddTaskSaveClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(this.Context, Resource.String.networkAccessAlert, ToastLength.Short).Show();
            }
        }
    }
}