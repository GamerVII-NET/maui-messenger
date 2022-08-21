using Messenger.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Domains.Models
{
    public class Attachment
    {
        [Key]
        public Guid Guid { get; set; }

        public AttachmentType Type { get; set; }

        public string Hash { get; set; } = string.Empty;

        public string Payload { get; set; } = string.Empty;
    }
}
