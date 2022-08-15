using Enums;

namespace Messenger.Domains.Models
{
    public class ChatModel
    {
        public UserModel User { get; set; }

        public string LastMessage { get; set; }

        public DateTime LastMessageDate { get; set; }

        public MessageType MessageType { get; set; }

        public bool IsOnline { get; set; }
        public int MessageCount { get; set; }
    }
}
