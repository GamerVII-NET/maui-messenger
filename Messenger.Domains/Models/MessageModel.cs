using Messenger.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domains.Models
{
    public class MessageModel
    {
        public Guid MessageGuid { get; set; }

        public Guid SenderGuid { get; set; }

        public MessageModel? ReplyTo { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime SendingTime { get; set; }

        public IEnumerable<UserModel> UserRead { get; set; } = Enumerable.Empty<UserModel>();
        
        public IEnumerable<AttachmentModel> Attachments { get; set; } = Enumerable.Empty<AttachmentModel>();
    }
}
