using IllyaVirych.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace IllyaVirych.WPF.Services
{
    public class LoginService : ILoginService
    {
        public Action OnLoggedInHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public OAuth2Authenticator Authhenticator()
        {
            throw new NotImplementedException();
        }

        public void LoginInstagram()
        {
            throw new NotImplementedException();
        }

        public void LogoutInstagram()
        {
            throw new NotImplementedException();
        }
    }
}
