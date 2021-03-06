using CoreGraphics;
using Foundation;
using IllyaVirych.Core.Helper;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using System.Net.Http;
using WebKit;

namespace IllyaVirych.IOS
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class LoginWebView : MvxViewController<LoginWebViewModel>, IWKNavigationDelegate
    {
        #region Variables
        private NSUrlRequest _request;
        private WKWebViewConfiguration _configuration;
        private CGRect _cGRect;
        private WKWebView _webView;
        #endregion

        #region Constructors
        public LoginWebView() : base(nameof(LoginWebView), null)
        {
        }
        #endregion

        #region Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.LoginCommand.Execute();
            NavigationController.SetNavigationBarHidden(true, false);
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
            var url = new NSUrl("https://www.instagram.com/oauth/authorize/?client_id=a418ad2a645c485cb534972d5b3d8715&redirect_uri=https://www.google.com.ua/&response_type=token&scope=basic");
            _request = new NSUrlRequest(url);
            _webView.LoadRequest(_request);
        }
        #endregion

        #region Methods
        [Export("webView:didFinishNavigation:")]
        public async void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            NSUrl token = webView.Url;
            var accessToken = token.AbsoluteString;
            string[] cutAccessToken = accessToken.Split('=');
            if (cutAccessToken[0] == "https://www.google.com.ua/#access_token")
            {
                var instUrl = new NSUrl("https://api.instagram.com/v1/users/self/?access_token=" + cutAccessToken[1]);
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(instUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                responseBody.Split(responseBody[12]);
                var jobject = JObject.Parse(responseBody);
                var id_user = jobject["data"]["id"]?.ToString();
                UserInstagramId.SetUserId(id_user);
                ViewModel.LoginNaVigationAndCreateCommand.Execute();
            }
        }
        #endregion
    }
}