using Enums;

namespace Messenger.Domains.Dtos.Chat
{
    public class ChatReadFromUser
    {
        public Guid GlobalGuid { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public string About { get; set; }

        public ChatType Type { get; set; }
    }
}
