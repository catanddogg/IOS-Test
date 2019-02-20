using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using IllyaVirych.Core.Interface;
using UIKit;

namespace IllyaVirych.IOS.Services
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string messege)
        {
            UIAlertView error = new UIAlertView("", messege, null, "Ok", null);
            error.Show();
        }
    }
}