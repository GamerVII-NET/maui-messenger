using Enums;
using Messenger.Domains.Dtos.User;

namespace Messenger.Domains.Dtos.Chat
{
    public class ChatCreateDto
    {
        public UserUpdateDto Author { get; set; }
        
        public ChatNewDto Chat { get; set; }

        public ChatType Type { get; set; }
    }
}
