using IllyaVirych.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IllyaVirych.Core.Helper;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IllyaVirych.Xamarin.UI.Views
{
    [MvxModalPresentation]
    public partial class LoginWebPage : MvxContentPage<LoginWebViewModel>
	{
		public LoginWebPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
      
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.LoginCommand.Execute();
        }

        public async void webviewNavigated(WebView sender, WebNavigatedEventArgs e)
        {
            var accessToken = e.Url;
            string[] cutAccessToken = accessToken.Split('=');
            if(cutAccessToken[0] == "https://www.google.com.ua/#access_token")
            {
                var instAccessTokenUrl = new Uri("https://api.instagram.com/v1/users/self/?access_token=" + cutAccessToken[1]);
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(instAccessTokenUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                responseBody.Split(responseBody[12]);
                var jobject = JObject.Parse(responseBody);
                var id_user = jobject["data"]["id"]?.ToString();
                UserInstagramId.SetUserId(id_user);
                ViewModel.LoginNaVigationAndCreateCommand.Execute();
            }
        }
    }
}