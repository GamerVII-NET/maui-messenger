using Enums;
using Messenger.Domains.Models;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Domains.Dtos.Chat
{
    public class ChatNewDto
    {
        public string Name { get; set; }

        public string Photo { get; set; }

        public string About { get; set; }

        public ChatType Type { get; set; }
    }
}
