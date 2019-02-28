using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.Helper;
using IllyaVirych.Core.Interface;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using Xamarin.Auth;
using Android.Webkit;

namespace IllyaVirych.Xamarin.UI.Droid.Services
{
    public class LoginService : ILoginService
    {
        public Action OnLoggedInHandler { get; set; }

        public void LoginInstagram()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
            //UrlCache.SharedCache.RemoveAllCachedResponses();
            //NSUrlCache.SharedCache.MemoryCapacity = 0;
            //NSUrlCache.SharedCache.DiskCapacity = 0;

            //var websiteDataTypes = new NSSet<NSString>(new[]
            //{
            //    WKWebsiteDataType.Cookies,
            //    WKWebsiteDataType.DiskCache,
            //    WKWebsiteDataType.IndexedDBDatabases,
            //    WKWebsiteDataType.LocalStorage,
            //    WKWebsiteDataType.MemoryCache,
            //    WKWebsiteDataType.OfflineWebApplicationCache,
            //    WKWebsiteDataType.SessionStorage,
            //    WKWebsiteDataType.WebSQLDatabases
            //});

            //WKWebsiteDataStore.DefaultDataStore.FetchDataRecordsOfTypes(websiteDataTypes, (NSArray records) =>
            //{
            //    for (nuint i = 0; i < records.Count; i++)
            //    {
            //        var record = records.GetItem<WKWebsiteDataRecord>(i);
            //        WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(record.DataTypes,
            //                                                              new[] { record }, () => { Console.WriteLine($"deleted: {record.DisplayName}"); });
            //    }
            //});

            //NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            //foreach (var cookie in CookieStorage.Cookies)
            //{
            //    CookieStorage.DeleteCookie(cookie);
            //}

        }

        public void LogoutInstagram()
        {
            if (CrossSettings.Current.Contains("id") == true)
            {
                CrossSettings.Current.Clear();
            }
        }
        //Not Use
        public OAuth2Authenticator Authhenticator()
        {
            return null;
        }
    }
}