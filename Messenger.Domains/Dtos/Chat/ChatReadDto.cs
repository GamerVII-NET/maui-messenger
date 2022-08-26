using Enums;
using Messenger.Domains.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domains.Dtos.Chat
{
    public class ChatReadDto
    {
        public string Name { get; set; }

        public string Photo { get; set; }

        public string About { get; set; }

        public ChatType Type { get; set; }

        public IEnumerable<ChatUser> Users { get; set; } = Enumerable.Empty<ChatUser>();
    }
}
