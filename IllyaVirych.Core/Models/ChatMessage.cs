using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Models
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime Time { get; set; }
        public string Picture { get; set; }
        public bool IsOriginNative { get; set; }
    }
}
