﻿using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Helper
{
    public class UserInstagramId
    {       
        public static string GetUserId()
        {
            string userId = CrossSettings.Current.GetValueOrDefault("id", string.Empty);

            return userId;
        }

        public static void SetUserId(string userId)
        {
            CrossSettings.Current.AddOrUpdateValue("id", userId);
        }
    }
}
