using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using Java.Net;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UIKit;
using WebKit;
using Xamarin.Auth;

namespace IllyaVirych.IOS
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class TestLoginView : MvxViewController<TestLoginViewModel>, IWKNavigationDelegate
    {
        private NSUrlRequest _request;
        private WKWebViewConfiguration _configuration;
        private CGRect _cGRect;
        private WKWebView _webView;
      
        public TestLoginView() : base(nameof(TestLoginView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();          
            NSUrlCache.SharedCache.RemoveAllCachedResponses();           
            NSUrlCache.SharedCache.MemoryCapacity = 0;
            NSUrlCache.SharedCache.DiskCapacity = 0;

            var websiteDataTypes = new NSSet<NSString>(new[]
            {
                WKWebsiteDataType.Cookies,
                WKWebsiteDataType.DiskCache,
                WKWebsiteDataType.IndexedDBDatabases,
                WKWebsiteDataType.LocalStorage,
                WKWebsiteDataType.MemoryCache,
                WKWebsiteDataType.OfflineWebApplicationCache,
                WKWebsiteDataType.SessionStorage,
                WKWebsiteDataType.WebSQLDatabases
            });

            WKWebsiteDataStore.DefaultDataStore.FetchDataRecordsOfTypes(websiteDataTypes, (NSArray records) =>
            {
                for (nuint i = 0; i < records.Count; i++)
                {
                    var record = records.GetItem<WKWebsiteDataRecord>(i);
                    WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(record.DataTypes,
                                                                          new[] { record }, () => { Console.WriteLine($"deleted: {record.DisplayName}"); });
                }
            });

            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
            }

            _configuration = new WKWebViewConfiguration();
            _cGRect = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
            _webView = new WKWebView(_cGRect, _configuration);  
            LoginWebView = _webView;
            _webView.TranslatesAutoresizingMaskIntoConstraints = false;
            _webView.NavigationDelegate = (IWKNavigationDelegate)this;
            LoginWebView = _webView;
            View = LoginWebView;          
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
                //AccountStore.Create().Save(loggedInAccount, "InstagramUser");                    
                ViewModel.ListTaskTaskCommand.Execute();
            }
        }       
    }
}