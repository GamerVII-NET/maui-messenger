using Messenger.Domains.Dtos.User;
using Messenger.Domains.Models;

namespace Messenger.Domains.Dtos.Messages
{
    public class MessageChatReadDto
    {
        public Guid GlobalGuid { get; set; }

        public UserReadResponseDto Sender { get; set; }

        public MessageChatReadDto? ReplyTo { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? UpdatedAt { get; set; }

        public DateTime SendingTime { get; set; }

        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public string Text { get; set; }
    }
}
