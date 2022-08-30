using Messenger.Domains.Dtos.Chat;
using Messenger.Domains.Dtos.User;
using Messenger.Domains.Models;

namespace Messenger.Domains.Dtos.Messages
{
    public class MessageCreateDto
    {

        public UserConnectDto Sender { get; set; }

        public Message? ReplyTo { get; set; }

        public ChatConnectDto Chat { get; set; }

        public List<Attachment>? Attachments { get; set; }

        public string Text { get; set; }
    }
}
