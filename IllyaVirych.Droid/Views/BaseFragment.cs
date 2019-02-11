using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Android.Support.V7.Widget;
using Android.Content.Res;
using MvvmCross.ViewModels;
using Android.Support.V4.Widget;

namespace IllyaVirych.Droid.ViewModels
{
    public abstract class BaseFragment : MvxFragment
    {
        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;
        protected abstract int FragmentId { get; }
        private bool _enabledDrawerLayout;

        public MvxAppCompatActivity ParentActivity
        {
            get
            {
                return (MvxAppCompatActivity)Activity;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentId, null);
            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            if (_toolbar != null)
            {
                ParentActivity.SetSupportActionBar(_toolbar);
                ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                _drawerToggle = new MvxActionBarDrawerToggle(
                    Activity,
                    ((MainView)ParentActivity).DrawerLayout,
                    _toolbar,
                    Resource.String.drawer_open,
                    Resource.String.drawer_close
                    );
                _drawerToggle.DrawerOpened += (object sender, ActionBarDrawerEventArgs e) => ((MainView)Activity)?.HideSoftKeyboard();
                ((MainView)ParentActivity).DrawerLayout.AddDrawerListener(_drawerToggle);
            }
            EnableDrawerLayout();

            return view;
        }       
        public void EnableDrawerLayout()
        {
            if (FragmentId == Resource.Layout.LoginView || FragmentId == Resource.Layout.MapsView)
            {
                _enabledDrawerLayout = false;
            }
            if (FragmentId != Resource.Layout.LoginView & FragmentId != Resource.Layout.MapsView)
            {
                _enabledDrawerLayout = true;
            }
            int lockMode = _enabledDrawerLayout ? DrawerLayout.LockModeUnlocked : DrawerLayout.LockModeLockedClosed;
            ((MainView)Activity).DrawerLayout.SetDrawerLockMode(lockMode);
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (FragmentId != Resource.Layout.LoginView)
            {
                if (_toolbar != null)
                {
                    _drawerToggle.OnConfigurationChanged(newConfig);
                }
            }
        }        
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            if (FragmentId != Resource.Layout.LoginView)
            {
                if (_toolbar != null)
                {
                    _drawerToggle.SyncState();
                }
            }
        }
    }
    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}