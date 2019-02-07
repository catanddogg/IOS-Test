using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Models
{
    public class CurrentInstagramUser
    {
        private static string _currentInstagramUserId = string.Empty;

        public static string CurrentInstagramUserId
        {
            get
            {
                return _currentInstagramUserId;
            }
            set
            {
                _currentInstagramUserId = value;
            }
        }
    }
}
