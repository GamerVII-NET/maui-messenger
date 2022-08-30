using Enums;
using Messenger.Domains.Dtos.Messages;

namespace Messenger.Domains.Dtos.Chat
{
    public class ChatReadFromUserDto
    {
        public Guid GlobalGuid { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public string About { get; set; }

        public ChatType Type { get; set; }

        public List<MessageChatReadDto> Messages { get; set; }
    }
}
