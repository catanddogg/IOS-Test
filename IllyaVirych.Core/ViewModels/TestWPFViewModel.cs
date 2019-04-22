using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Messenger;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.Services;
using Microsoft.AspNet.SignalR.Client;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using static IllyaVirych.Core.Models.Enums;

namespace IllyaVirych.Core.ViewModels
{
    //WPF
    public class TestWPFViewModel : BaseViewModel<string>, ISignalRDelegate
    {
        #region Variables
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _token;

        private const string ServerURI = "http://localhost:65015/chat";

        private ISingnalRPersistensConnectionService _singnalRPersistensConnectionService;
        private List<string> _nameGroups;
        private TaskFactory ctxTaskFactory; 
        private ISignalRDelegate Delegate { get; set; }
        private IAlertService _alertService;
        #endregion

        #region Constructors
        public TestWPFViewModel(IMvxNavigationService mvxNavigationService, ISingnalRPersistensConnectionService singnalRPersistensConnectionService, IMvxMessenger messenger, IAlertService alertService)
            :base(mvxNavigationService)
        {
            _alertService = alertService;
            _singnalRPersistensConnectionService = singnalRPersistensConnectionService;
            _singnalRPersistensConnectionService.Delegate = this;
            _messenger = messenger;
            _token = messenger.Subscribe<CloseEventTestMessenger>(CloseApplication);
            _nameGroups = new List<string>();
            ListChatUser = new ObservableCollection<ChatUser>();
            SelectedParticipant = new ChatUser();
            SendButtomCommand = new MvxAsyncCommand(SendMessage);
            ctxTaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
        }
        #endregion

        #region Properties
        private ObservableCollection<ChatUser> _listChatUser = new ObservableCollection<ChatUser>();
        public ObservableCollection<ChatUser> ListChatUser
        {
            get
            {
                return _listChatUser;
            }
            set
            {
                _listChatUser = value;
                RaisePropertyChanged(() => ListChatUser);
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _statusText;
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                _statusText = value;
                RaisePropertyChanged(() => StatusText);
            }
        }

        private string _textBoxMessage;
        public string TextBoxMessage
        {
            get
            {
                return _textBoxMessage;
            }
            set
            {
                _textBoxMessage = value;
                RaisePropertyChanged(() => TextBoxMessage);
            }
        }

        private bool _isLoggedIn = true;
        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }
            set
            {
                _isLoggedIn = value;
                RaisePropertyChanged(() => IsLoggedIn);
            }
        }

        private ChatUser _selectedParticipant;
        public ChatUser SelectedParticipant
        {
            get
            {
                return _selectedParticipant;
            }
            set
            {
                try
                {
                    _selectedParticipant = value;
                    if (SelectedParticipant.HasSentNewMessage) SelectedParticipant.HasSentNewMessage = false;
                    RaisePropertyChanged(() => SelectedParticipant);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        #endregion

        #region Commands
        public IMvxAsyncCommand SendButtomCommand { get; set; }
        #endregion

        #region Methods
        private async void CloseApplication(CloseEventTestMessenger mapMesseger)
        {
            try
            {
                await _singnalRPersistensConnectionService.SendMessageAsync(UserName, null, MethodType.RemoveUser, null, ReceiverType.None);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task SendMessage()
        {
            try
            {
                string nameUserOrGroupWhoReceiveMessage =  null;
                if (TextBoxMessage == null || TextBoxMessage == string.Empty)
                {
                    return;
                }
                if (SelectedParticipant.Name != null)
                {
                     nameUserOrGroupWhoReceiveMessage = SelectedParticipant.Name;
                }
                if (nameUserOrGroupWhoReceiveMessage != null && SelectedParticipant.receiverType == ReceiverType.User)
                {
                    await _singnalRPersistensConnectionService.SendMessageAsync(UserName, TextBoxMessage, MethodType.Send, nameUserOrGroupWhoReceiveMessage, ReceiverType.User);
                    ChatUser sender = new ChatUser();
                    ChatMessage chatMessage = new ChatMessage { Author = UserName, Message = TextBoxMessage, Time = DateTime.Now, IsOriginNative = true };

                    sender = _listChatUser.Where((u) => string.Equals(u.Name, nameUserOrGroupWhoReceiveMessage)).FirstOrDefault();
                    await ctxTaskFactory.StartNew(() => sender.Chatter.Add(chatMessage));

                    if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
                    {
                         await ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true);
                    }
                    TextBoxMessage = string.Empty;
                }
                if (nameUserOrGroupWhoReceiveMessage != null && SelectedParticipant.receiverType == ReceiverType.Group)
                {
                    await _singnalRPersistensConnectionService.SendMessageAsync(UserName, TextBoxMessage, MethodType.Send, nameUserOrGroupWhoReceiveMessage, ReceiverType.Group);
                    TextBoxMessage = string.Empty;
                }
            }
            catch(Exception e)
            {
                if(e.Message == "Data cannot be sent because the WebSocket connection is reconnecting.")
                {
                    //_alertService.ShowAlert("You have a problem with the internet");
                    for (int i = 0; i < _listChatUser.Count; i++)
                    {
                         var sender = _listChatUser.Where((u) => string.Equals(u.Name, _listChatUser[i].Name)).FirstOrDefault();
                         await ctxTaskFactory.StartNew(() => sender.IsLoggedIn = false);
                    }
                    var reconnectStatus = await _singnalRPersistensConnectionService.ReconnectServerAsync(ServerURI);
                    if(reconnectStatus)
                    {
                        await _singnalRPersistensConnectionService.SendMessageAsync(UserName, null, MethodType.AddUser, null, ReceiverType.None);
                    }
                    if(!reconnectStatus)
                    {
                        await _navigationService.Navigate<WPFLoginViewModel>();
                    }
                }
            }
        }

        public async void OnReceivedMessage(ReceivedMessage data)
        {
            if (data.ReceiverType == ReceiverType.User)
            {
                if (data.ReceiverId == UserName)
                {
                    ChatUser sender = new ChatUser();
                    ChatMessage chatMessage = new ChatMessage { Author = data.ReceiverId, Message = data.Message, Time = DateTime.Now };
                    sender = _listChatUser.Where((u) => string.Equals(u.Name, data.SenderId)).FirstOrDefault();
                    await ctxTaskFactory.StartNew(() => sender.Chatter.Add(chatMessage));                    
                    if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
                    {
                         await ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true);
                    }
                }
            }
            if (data.ReceiverType == ReceiverType.Group)
            {
                ChatUser sender = new ChatUser();
                ChatMessage chatMessage = new ChatMessage();
                if (data.SenderId != UserName)
                {
                    chatMessage = new ChatMessage { Author = data.SenderId, Message = data.Message, Time = DateTime.Now };
                }
                if (data.SenderId == UserName)
                {
                    chatMessage = new ChatMessage { Author = data.SenderId, Message = data.Message, Time = DateTime.Now, IsOriginNative = true };
                }
                sender = _listChatUser.Where((u) => string.Equals(u.Name, data.ReceiverGroup)).FirstOrDefault();
                await ctxTaskFactory.StartNew(() => sender.Chatter.Add(chatMessage));
                if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
                {
                    await ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true);
                }
            }
        }

        public void OnUserListChanged(List<ChatRooms> chatRooms)
        {
            for (int j = 0; j < chatRooms.Count; j++)
            {
                if (chatRooms[j].ChatRoomName == UserName)
                {
                    chatRooms.RemoveAt(j);
                }
            }
            List<ChatUser> users = new List<ChatUser>();
            for (int i = 0; i < chatRooms.Count; i++)
            {
                users.Add(new ChatUser { Name = chatRooms[i].ChatRoomName, Photo = null, HasSentNewMessage = false, IsLoggedIn = true, IsTyping = false, receiverType = chatRooms[i].ReceiverType });
            }
            ListChatUser = new ObservableCollection<ChatUser>(users);
        }

        public override void Prepare(string parameter)
        {
            UserName = parameter;
        }

        #endregion
    }
}