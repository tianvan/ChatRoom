using System;

namespace ChatRoom.Client.Models
{
    public class Message
    {
        public string Avatar { get; set; }

        public string Nickname { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }

        public bool IsOwned { get; set; }

        public MessageType Type { get; set; }
    }
}
