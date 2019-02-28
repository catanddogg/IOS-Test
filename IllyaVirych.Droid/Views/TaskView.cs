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
        #region Variables
        private RelativeLayout _linearLayoutMain;
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private Button _buttonTextSaveTask;
        private Button _buttonDeleteTask;
        private Button _buttonDeleteMarker;
        #endregion

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
           
            _buttonTextSaveTask = view.FindViewById<Button>(Resource.Id.Savetask);
            _buttonDeleteTask = view.FindViewById<Button>(Resource.Id.Deletetask);
            _buttonDeleteMarker = view.FindViewById<Button>(Resource.Id.DeleteMarker);
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
            Typeface tf = Typeface.CreateFromAsset(Activity.Assets, Resources.GetString(Resource.String.fontname));
            txtTaskView.SetTypeface(tf, TypefaceStyle.Normal);
            txtNameTaskView.SetTypeface(tf, TypefaceStyle.Normal);

            return view;
        }
        #endregion

        #region Override     
        protected override int FragmentId => Resource.Layout.TaskView;
        #endregion

        #region Methods
        public void HideSoftKeyboard()
        {
            InputMethodManager close = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            close.HideSoftInputFromWindow(_linearLayoutMain.WindowToken, 0);
        }
        #endregion
    }
}