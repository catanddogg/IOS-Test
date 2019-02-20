using System;
using Foundation;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using Plugin.Settings;
using WebKit;
using Xamarin.Auth;

namespace IllyaVirych.IOS.Services
{
    public class LoginService : ILoginService
    {        
        public Action OnLoggedInHandler { get; set; }
       // private Account _findaccountforservice;      
        //private string _string;

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