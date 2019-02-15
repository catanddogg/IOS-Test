using Android.OS;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Xamarin.Essentials;

namespace IllyaVirych.Droid.ViewModels
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class LoginView : BaseFragment<LoginViewModel>
    {      
        protected override int FragmentId => Resource.Layout.LoginView;
        private ImageButton _imageButton;              

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            _imageButton = view.FindViewById<ImageButton>(Resource.Id.image_button);
            

            _imageButton.Click += delegate
            {
                var networkAccess = this.ViewModel.NetworkAccess;
                if (networkAccess != NetworkAccess.Internet)
                {
                    Toast.MakeText(this.Context, "You do not have network access!", ToastLength.Short).Show();
                    return;
                }
                InstagramLogin();
            };            
            return view;
        }
        
        public void InstagramLogin()
        {
            ViewModel.LoginCommand.Execute();
            StartActivity(ViewModel.Authhenticator.GetUI(this.Context));            
        }          
    }
}