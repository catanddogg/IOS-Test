using IllyaVirych.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static IllyaVirych.Core.Models.Enums;

namespace IllyaVirych.Core.Interface
{
    public interface ISignalRDelegate 
    {
        void OnReceivedMessage(ReceivedMessage data);

        void OnUserListChanged(List<ChatRooms> chatRooms);
    }

    public class ReceivedMessage
    {
        public string SenderId;
        public ReceiverType ReceiverType;
        public string Message;
        public string ReceiverId;
        public string ReceiverGroup;
    }
}
