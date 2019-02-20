using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;


namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame, true)]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        #region Variables
        private NavigationView _navigationView;
        #endregion

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(Resource.Layout.MenuView, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_menu_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            return view;
        }
        #endregion

        #region Methods
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Navigate(item.ItemId);
            return true;
        }
        private async void Navigate(int itemId)
        {
            ((MainView)Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));
            if (itemId == Resource.Id.nav_task)
            {
                ViewModel.TaskCreateViewCommand.Execute(null);
            }
            if (itemId == Resource.Id.nav_about)
            {
                ViewModel.AboutViewCommand.Execute(null);
            }
            if (itemId == Resource.Id.nav_login)
            {
                ViewModel.LoginViewCommand.Execute(null);
            }
        }
        #endregion
    }
}