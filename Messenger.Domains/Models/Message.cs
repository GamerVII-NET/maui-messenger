using Messenger.Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domains.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public Guid GlobalGuid { get; set; }

        public User Sender { get; set; }

        public Message? ReplyTo { get; set; }

        public Chat Chat { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? UpdatedAt { get; set; }

        public DateTime SendingTime { get; set; }

        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public string Text { get; set; }
    }
}
