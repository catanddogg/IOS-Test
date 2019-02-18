using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Widget;
using Xamarin.Essentials;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class TaskView : BaseFragment<TaskViewModel>
    {
        protected override int FragmentId => Resource.Layout.TaskView;
        
        private RelativeLayout _linearLayoutMain;
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private View _view;        
        private readonly string _fontname = "13185.ttf";       

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _view = view;
            var buttonTextSaveTask = view.FindViewById<Button>(Resource.Id.Savetask);
            buttonTextSaveTask.Click += ButtonSaveTaskClick;
            var buttonDeleteMarker = view.FindViewById<Button>(Resource.Id.DeleteMarker);
            buttonDeleteMarker.Click += ButtonDeleteMarkerClick;
            _linearLayoutMain = view.FindViewById<RelativeLayout>(Resource.Id.test_layout);
            _toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar1);
            _linearLayoutMain.Click += delegate
            {
                HideSoftKeyboard();
            };
            _toolbar.Click += delegate
            {
                HideSoftKeyboard();
            };
            var txtTaskView = view.FindViewById<TextView>(Resource.Id.task_text);
            var txtNameTaskView = view.FindViewById<TextView>(Resource.Id.name_text);
            Typeface tf = Typeface.CreateFromAsset(Activity.Assets, _fontname);
            txtTaskView.SetTypeface(tf, TypefaceStyle.Normal);
            txtNameTaskView.SetTypeface(tf, TypefaceStyle.Normal);

            var buttonDeleteTask = view.FindViewById<Button>(Resource.Id.Deletetask);
            buttonDeleteTask.Click += ButtonDeleteTaskClick;
            
            return view;
        }

        
        private void ButtonDeleteTaskClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(this.Context, Resource.String.networkAccessAlert, ToastLength.Short).Show();
            }
        }

        private void ButtonDeleteMarkerClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(this.Context, Resource.String.networkAccessAlert, ToastLength.Short).Show();
                return;
            }
            var LalitudeMarker = this.ViewModel.LalitudeMarkerResult;
            if (LalitudeMarker == 0)
            {
                Toast.MakeText(this.Context, Resource.String.deleteMarkerAlert, ToastLength.Short).Show();
                return;
            }
            Toast.MakeText(this.Context, Resource.String.deleteMarkerAlertHasMarker, ToastLength.Short).Show();
        }

        private void ButtonSaveTaskClick(object sender, EventArgs e)
        {
            var networkAccess = this.ViewModel.NetworkAccess;
            if (networkAccess != NetworkAccess.Internet)
            {
                Toast.MakeText(this.Context, Resource.String.networkAccessAlert, ToastLength.Short).Show();
                return;
            }
            var nameTask = this.ViewModel.NameTask;
            if (nameTask == null)
            {
                Toast.MakeText(this.Context, Resource.String.saveTaskAlert, ToastLength.Short).Show();
            }
        }

        public void HideSoftKeyboard()
        {
            InputMethodManager close = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            close.HideSoftInputFromWindow(_linearLayoutMain.WindowToken, 0);
        }
    }
}