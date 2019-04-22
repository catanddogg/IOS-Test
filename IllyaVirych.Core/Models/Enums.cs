using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Models
{
    public class Enums
    {
        public enum ReceiverType
        {
            Group = 0,
            User = 1,
            None = 2
        }

        public enum MethodType
        {
            Send = 0,
            AddUser = 1,
            RemoveUser = 2
        }
    }
}
