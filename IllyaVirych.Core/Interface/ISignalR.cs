using IllyaVirych.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.Interface
{
    public interface ISignalR
    {
        Task Connect();
        Task Send(ChatMessage message, string roomName);
        Task JoinGroup(string roomName);
        event EventHandler<ChatMessage> OnMessageReceived;
    }
}
