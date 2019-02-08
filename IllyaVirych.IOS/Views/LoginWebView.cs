using CoreGraphics;
using Foundation;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using WebKit;
using Xamarin.Auth;

namespace IllyaVirych.IOS
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class LoginWebView : MvxViewController<LoginWebViewModel>, IWKNavigationDelegate
    {
        private NSUrlRequest _request;
        private WKWebViewConfiguration _configuration;
        private CGRect _cGRect;
        private WKWebView _webView;

        public LoginWebView () : base(nameof(LoginWebView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.LoginCommand.Execute();
            _configuration = new WKWebViewConfiguration();
            _cGRect = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
            _webView = new WKWebView(_cGRect, _configuration);
            _webView.TranslatesAutoresizingMaskIntoConstraints = false;
            _webView.NavigationDelegate = (IWKNavigationDelegate)this;
            LoginInstagramWebView = _webView;
            View = LoginInstagramWebView;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var url = new NSUrl("https://www.instagram.com/oauth/authorize/?client_id=f0c8c1093c06475dbeadba39c6b3ac80&redirect_uri=https://www.google.com.ua/&response_type=token&scope=basic");
            _request = new NSUrlRequest(url);
            _webView.LoadRequest(_request);
        }

        [Export("webView:didFinishNavigation:")]
        public async void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            NSUrl token = webView.Url;
            var accesstoken = token.AbsoluteString;
            if (accesstoken != "https://www.instagram.com/accounts/login/?next=/oauth/authorize/%3Fclient_id%3Df0c8c1093c06475dbeadba39c6b3ac80%26redirect_uri%3Dhttps%3A//www.google.com.ua/%26response_type%3Dtoken%26scope%3Dbasic"
                & accesstoken != "https://www.instagram.com/accounts/onetap/?next=%2Foauth%2Fauthorize%2F%3Fclient_id%3Df0c8c1093c06475dbeadba39c6b3ac80%26redirect_uri%3Dhttps%3A%2F%2Fwww.google.com.ua%2F%26response_type%3Dtoken%26scope%3Dbasic")
            {
                var instUrl = new NSUrl("https://api.instagram.com/v1/users/self/?access_token=10368663437.f0c8c10.074f236d65e84089a87183db799a9cbb");
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(instUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var jobject = JObject.Parse(responseBody);
                var id_user = jobject["data"]["id"]?.ToString();
                Account loggedInAccount = new Account();
                loggedInAccount.Properties.Add("id", id_user);
                CurrentInstagramUser.CurrentInstagramUserId = id_user;
                AccountStore.Create().Save(loggedInAccount, "InstagramUser");   
                ViewModel.LoginNaVigationAndCreateCommand.Execute();
            }
        }
    }
}