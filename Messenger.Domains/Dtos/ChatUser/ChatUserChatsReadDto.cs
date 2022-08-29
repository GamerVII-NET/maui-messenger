using Messenger.Domains.Dtos.Chat;
using Messenger.Domains.Dtos.User;
using Messenger.Domains.Enums;

namespace Messenger.Domains.Dtos.ChatUser
{
    public class ChatUserChatsReadDto
    {
        public ChatReadFromUser Chat { get; set; }

        public Guid GlobalGuid { get; set; }

        public bool Deleted { get; set; } = false;

        public bool Banned { get; set; } = false;

        public bool IsMuted { get; set; } = false;

        public ChatRole UserRole { get; set; } = ChatRole.Default;

        public DateTime Joined { get; set; } = DateTime.Now;

        public DateTime? MuteEnd { get; set; }
    }
}
