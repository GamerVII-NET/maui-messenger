using Messenger.Domains.Dtos.User;
using Messenger.Domains.Models;

namespace Messenger.Domains.Dtos.Messages
{
    public class MessageEditDto
    {

        public Guid GlobalGuid { get; set; }

        public UserConnectDto Sender { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public string Text { get; set; }
    }
}
