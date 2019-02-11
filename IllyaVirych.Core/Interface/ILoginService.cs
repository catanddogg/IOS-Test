using System;
using Xamarin.Auth;

namespace IllyaVirych.Core.Interface
{
    public interface ILoginService
    {
        void LoginInstagram();
        OAuth2Authenticator Authhenticator();
        Action OnLoggedInHandler { get; set; }
        void LogoutInstagram();       
    }
}
