using IllyaVirych.Core.Interface;
using IllyaVirych.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using static IllyaVirych.Core.Models.Enums;

namespace IllyaVirych.Core.Models
{
    public class ChatUser : ViewModelBase
    {
        public string Name { get; set; }
        public byte[] Photo { get; set; }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        private bool _hasSentNewMessage;
        public bool HasSentNewMessage
        {
            get
            {
                return _hasSentNewMessage;
            }
            set
            {
                _hasSentNewMessage = value;
                OnPropertyChanged();
            }
        }
        public bool IsTyping { get; set; }
        public ReceiverType receiverType { get; set; }
        public ObservableCollection<ChatMessage> Chatter { get; set; }

        public ChatUser()
        {
            Chatter = new ObservableCollection<ChatMessage>();
        }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName()] string name = null)
        {
            if (name != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
