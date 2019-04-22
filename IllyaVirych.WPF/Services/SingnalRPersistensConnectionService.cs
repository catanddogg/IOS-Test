using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using MvvmCross;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using static IllyaVirych.Core.Models.Enums;
using System.Threading;

namespace IllyaVirych.WPF.Services
{
    public class SingnalRPersistensConnectionService : ISingnalRPersistensConnectionService
    {
        private Connection _connection;

        public ISignalRDelegate Delegate { get; set; } 

        public async Task ConnectServerAsync(string serverURI)
        {
            _connection = new Connection(serverURI);
            await _connection.Start();
            _connection.Received += data =>
            {
                ChatData chatdata = JsonConvert.DeserializeObject<ChatData>(data);               
                ReceivedMessage receivedMessage = Mapper.Map<ReceivedMessage>(chatdata);
                if (chatdata.MethodType == MethodType.Send)
                {
                    Delegate?.OnReceivedMessage(receivedMessage);
                }
                if (chatdata.MethodType == MethodType.AddUser)
                {
                    List<ChatRooms> chatRooms = chatdata.ChatRooms;

                    Delegate?.OnUserListChanged(chatRooms);
                }
            };
        }

        public async Task<bool> ReconnectServerAsync(string serverURI)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    _connection = new Connection(serverURI);
                    await _connection.Start();
                    if (_connection != null)
                    {
                        _connection.Received += data =>
                        {
                            ChatData chatdata = JsonConvert.DeserializeObject<ChatData>(data);
                            ReceivedMessage receivedMessage = Mapper.Map<ReceivedMessage>(chatdata);
                            if (chatdata.MethodType == MethodType.Send)
                            {
                                Delegate?.OnReceivedMessage(receivedMessage);
                            }
                            if (chatdata.MethodType == MethodType.AddUser)
                            {
                                List<ChatRooms> chatRooms = chatdata.ChatRooms;

                                Delegate?.OnUserListChanged(chatRooms);
                            }
                        };
                        return true;
                    }
                }
                catch (Exception e)
                {
                    await Task.Run(() => {
                        Console.WriteLine(e);
                        Thread.Sleep(5000);
                    });
                }
            }
            return false;
        }

        public async Task SendMessageAsync(string receiverId, string message, MethodType methodType, string senderId, ReceiverType receiverType)
        {
            if (_connection != null)
            {
                ChatData chatData = new ChatData() { SenderId =  receiverId, Message = message, ReceiverType = receiverType, ReceiverId = senderId, MethodType = methodType };
                await _connection.Send(chatData);
            }
        }
    }
}
