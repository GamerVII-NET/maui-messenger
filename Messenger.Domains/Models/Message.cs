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
        public Guid Guid { get; set; }

        public Guid SenderGuid { get; set; }

        public Message? ReplyTo { get; set; }

        public Chat ChatModel { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime SendingTime { get; set; }

    }
}
