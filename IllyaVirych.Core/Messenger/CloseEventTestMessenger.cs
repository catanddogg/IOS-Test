using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Messenger
{
    public class CloseEventTestMessenger : MvxMessage
    {
        public CloseEventTestMessenger(object sender ): base(sender)
        {

        }
    }
}
