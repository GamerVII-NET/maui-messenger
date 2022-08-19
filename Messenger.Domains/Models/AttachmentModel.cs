using Messenger.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domains.Models
{
    public class AttachmentModel
    {
        public AttachmentType Type { get; set; }

        public string Hash { get; set; } = string.Empty;

        public object Payload { get; set; } = string.Empty;
    }
}
