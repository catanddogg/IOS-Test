using Android.OS;
using Android.Views;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class AboutView : BaseFragment<AboutTaskViewModel>
    {
        protected override int FragmentId => Resource.Layout.AboutView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);      
            
            return view;
        }      
    }
}