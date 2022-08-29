using Messenger.Domains.Dtos.Chat;
using Messenger.Domains.Dtos.User;

namespace Messenger.Domains.Dtos.Links
{
    public class UserInChatDto
    {
        public ChatConnectDto Chat { get; set; }

        public UserConnectDto User { get; set; }

    }
}
