using Enums;
using Messenger.Domains.Dtos.ChatUser;
using Messenger.Domains.Dtos.User;

namespace Messenger.Domains.Dtos.Chat
{
    public class ChatCreateDto
    {
        public ChatUserCreateDto Inviter { get; set; }

        public List<ChatUserCreateDto> Users { get; set; }
        
        public ChatNewDto Chat { get; set; }
    }
}
