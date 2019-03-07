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
        #region Variables
        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;
        protected abstract int FragmentId { get; }
        private bool _enabledDrawerLayout;
        #endregion

        #region Lifecycle
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentId, null);
            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            SetUpToolbar();
            SetUpDrawerLayout();

            return view;
        }
        #endregion

        #region Override 
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
        #endregion

        #region Properties
        public MvxAppCompatActivity ParentActivity
        {
            get
            {
                return (MvxAppCompatActivity)Activity;
            }
        }
        #endregion

        #region Methods
        private void SetUpToolbar()
        {
            if (_toolbar != null)
            {
                ParentActivity.SetSupportActionBar(_toolbar);
                _toolbar.SetNavigationIcon(Resource.Drawable.baseline_add_location_black_48dp);
                ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                ParentActivity.SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(false);
                _drawerToggle = new MvxActionBarDrawerToggle(
                    Activity,
                    ((MainView)ParentActivity).DrawerLayout,
                    _toolbar,
                    Resource.String.drawer_open,
                    Resource.String.drawer_close
                    );
                _drawerToggle.DrawerIndicatorEnabled = false;
                _drawerToggle.SetHomeAsUpIndicator(Resource.Drawable.baseline_add_location_black_48dp);
                _drawerToggle.DrawerOpened += (object sender, ActionBarDrawerEventArgs e) => ((MainView)Activity)?.HideSoftKeyboard();
                ((MainView)ParentActivity).DrawerLayout.AddDrawerListener(_drawerToggle);
                
            }
        }

        private void SetUpDrawerLayout()
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
        #endregion
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