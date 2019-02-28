using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.Services
{
    public class SignalR : ISignalR
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;

        public event EventHandler<ChatMessage> OnMessageReceived;

        public SignalR()
        {
            _hubConnection = new HubConnection("http://10.10.3.199:65015/");
            _hubProxy = _hubConnection.CreateHubProxy("TaskHub");            
            _hubProxy.On("GetMessage", (string name, string message) => OnMessageReceived(this, new ChatMessage
            {
                Name = name,
                Message = message
            }));
            
            OnMessageReceived += GetValue_Message;
        }

        private void GetValue_Message(object sender, ChatMessage e)
        {
            throw new NotImplementedException();
        }

        public async Task Connect()
        {
            var http = new Microsoft.AspNet.SignalR.Client.Http.DefaultHttpClient();
            //var transports = new List<IClientTransport>()
            //                                                        {
            //                                                            new WebSocketTransportLayer(http),
            //                                                            new ServerSentEventsTransport(http),
            //                                                            new LongPollingTransport(http)
            //                                                        };
            /// Preparando la conexion
            //await _connection.Start(new AutoTransport(http, transports));
            await _hubConnection.Start(/*new WebSocketTransportLayer(http)*/);
        }

        public async Task JoinGroup(string roomName)
        {
            if (_hubConnection.State == ConnectionState.Disconnected)
            {
                await Connect();
            }
            await _hubProxy.Invoke("JoinGroup", roomName);
        }

        public async Task Send(ChatMessage message, string roomName)
        {
            if (_hubConnection.State == ConnectionState.Disconnected)
            {
                await Connect();
            }
            await _hubProxy.Invoke("SendMessage", message.Name, message.Message, roomName);
        }
    }
}
