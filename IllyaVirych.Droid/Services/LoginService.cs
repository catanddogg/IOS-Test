using System;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using Xamarin.Auth;

namespace IllyaVirych.Droid.Services
{
    public class LoginService : ILoginService
    {
        private OAuth2Authenticator _auth;
        public Action OnLoggedInHandler { get; set; }
        private Account _findaccountforservice;
        public void LoginInstagram()
        {
            _auth = new OAuth2Authenticator
                (
                clientId: "f0c8c1093c06475dbeadba39c6b3ac80",
                scope: "basic",
                authorizeUrl: new Uri("https://www.instagram.com/oauth/authorize/?client_id=f0c8c1093c06475dbeadba39c6b3ac80&redirect"),
                redirectUrl: new Uri("https://www.google.com.ua/")
                );
            _auth.ClearCookiesBeforeLogin = true;
            _auth.AllowCancel = true;
            _auth.Completed += InstagramCompletedAutgenticated;
        }

        public void LogoutInstagram()
        {            
            if (CrossSettings.Current.Contains("id") == true)
            {
                CrossSettings.Current.Clear();                
                CurrentInstagramUser.CurrentInstagramUserId = null;
            }
        }       

        public async void InstagramCompletedAutgenticated(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                Account loggedInAccount = eventArgs.Account;
                var request = new OAuth2Request("GET",
                    new Uri("https://api.instagram.com/v1/users/self/?access_token=8496248657.f23b40b.fbb30e8c10ff4214ad833e2ea3035deb"),
                    null,
                    eventArgs.Account);
                var response = await request.GetResponseAsync();
                var json = response.GetResponseText();
                var jobject = JObject.Parse(json);
                var id_user = jobject["data"]["id"]?.ToString();
                CurrentInstagramUser.CurrentInstagramUserId = id_user;
                CrossSettings.Current.AddOrUpdateValue("id", id_user);
                OnLoggedInHandler();
            }
        }       
        
        public OAuth2Authenticator Authhenticator()
        {
            return _auth;
        }
    }
}