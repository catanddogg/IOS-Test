using Android.OS;
using Android.Views;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class AboutView : BaseFragment<AboutTaskViewModel>
    {
        #region Constructors       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);      
            
            return view;
        }
        #endregion

        #region Override
        protected override int FragmentId => Resource.Layout.AboutView;
        #endregion
    }
}