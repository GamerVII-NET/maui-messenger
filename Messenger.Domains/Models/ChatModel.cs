using Enums;

namespace Messenger.Domains.Models
{
    public class ChatModel
    {

        public Guid ChatGuid { get; set; }

        public ChatType Type { get; set; }

        public IEnumerable<UserModel> Users { get; set; } = Enumerable.Empty<UserModel>();
    }
}
