using Messenger.Domains.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messenger.Domains.Models
{
    [Keyless]
    [Table("ChatUsers")]
    public class ChatUser
    {
        public Guid GlobalGuid;

        public Chat Chat { get; set; }

        public User User { get; set; }

        public User? InviterUser { get; set; }

        public bool Deleted { get; set; }

        public bool Banned { get; set; }

        public bool IsMuted { get; set; }

        public ChatRole UserRole { get; set; }

        public DateTime Joined { get; set; }

        public DateTime MuteEnd { get; set; }

    }
}
