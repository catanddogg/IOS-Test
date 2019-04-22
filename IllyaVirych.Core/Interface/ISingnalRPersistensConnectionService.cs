using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using static IllyaVirych.Core.Models.Enums;

namespace IllyaVirych.Core.Interface
{
    public interface ISingnalRPersistensConnectionService
    {
        ISignalRDelegate Delegate { get; set; }

        Task ConnectServerAsync(string serverURI);

        Task SendMessageAsync(string receiverId, string message, MethodType methodType, string senderId, ReceiverType receiverType);

        Task<bool> ReconnectServerAsync(string serverURI);
    }  
}
