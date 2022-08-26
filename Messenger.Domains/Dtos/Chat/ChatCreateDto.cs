using Enums;
using Messenger.Domains.Dtos.User;

namespace Messenger.Domains.Dtos.Chat
{
    public class ChatCreateDto
    {
        public Guid GlobalGuid { get; set; }
        
        public ChatType Type { get; set; }

        public IEnumerable<UserReadDto> Users { get; set; } = Enumerable.Empty<UserReadDto>();
    }
}
