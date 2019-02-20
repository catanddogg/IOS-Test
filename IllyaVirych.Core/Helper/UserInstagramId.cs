using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Helper
{
    public class UserInstagramId
    {       
        public static string UserId()
        {
            string userId = CrossSettings.Current.GetValueOrDefault("id", string.Empty).ToString();

            return userId;
        }
    }
}
