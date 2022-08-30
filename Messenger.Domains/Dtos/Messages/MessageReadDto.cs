using Messenger.Domains.Dtos.Chat;
using Messenger.Domains.Dtos.User;
using Messenger.Domains.Models;

namespace Messenger.Domains.Dtos.Messages
{
    public class MessageReadDto
    {
        public Guid GlobalGuid { get; set; }

        public UserReadResponseDto Sender { get; set; }

        public MessageReadDto? ReplyTo { get; set; }

        public ChatReadFromUserDto Chat { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? UpdatedAt { get; set; }

        public DateTime SendingTime { get; set; }

        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public string Text { get; set; }
    }
}
