using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using CoreGraphics;
using Foundation;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using Newtonsoft.Json.Linq;
using UIKit;
using WebKit;
using Xamarin.Auth;

namespace IllyaVirych.IOS.Services
{
    public class LoginService : ILoginService
    {        
        public Action OnLoggedInHandler { get; set; }
        private Account _findaccountforservice;
        //private NSUrlRequest _request;
        //private WKWebViewConfiguration _configuration;
        //private CGRect _cGRect;
        //private WKWebView _webView;

        public void LoginInstagram()
        {
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
        }    
        
        public void LogoutInstagram()
        {
            var data = AccountStore.Create().FindAccountsForService("InstagramUser").FirstOrDefault();
            if (data != null)
            {
                AccountStore.Create().Delete(data, "InstagramUser");
                CurrentInstagramUser.CurrentInstagramUserId = null;
            }
        }
        
        public Account FindAccount
        {
            get
            {
                return _findaccountforservice = AccountStore.Create().FindAccountsForService("InstagramUser").FirstOrDefault();
            }
        }

        //Not Use
        public OAuth2Authenticator Authhenticator()
        {
            return null;
        }
    }
}