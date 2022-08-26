using Messenger.Domains.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messenger.Domains.Models
{
    [Table("Attachments")]
    public class Attachment
    {
        [Key]
        public Guid Guid { get; set; }

        public AttachmentType Type { get; set; }

        public string Hash { get; set; } = string.Empty;

        public string Payload { get; set; } = string.Empty;
    }
}
