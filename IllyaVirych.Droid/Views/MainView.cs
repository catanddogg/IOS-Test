using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Xamarin.Essentials;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxActivityPresentation]
    [Activity(Label = "RecycleView", MainLauncher = true)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        #region Variables
        #endregion
       
        #region Lifecycle
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Platform.Init(this, bundle);
            SetContentView(Resource.Layout.MainView);
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
            {
                ViewModel.CurrentMainViewCommand.Execute();
            }
        }
        #endregion

        #region Override   
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                DrawerLayout.OpenDrawer(GravityCompat.Start);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #endregion

        #region Properties
        public DrawerLayout DrawerLayout { get; set; }
        #endregion

        #region Methods
        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
            {
                return;
            }
            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
        #endregion
    }
}