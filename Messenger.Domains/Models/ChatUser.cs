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
        [Key]
        public Guid GlobalGuid;

        public virtual Chat Chat { get; set; }

        public virtual User User { get; set; }

        public User? InviterUser { get; set; }

        public bool Deleted { get; set; } = false;

        public bool Banned { get; set; } = false;

        public bool IsMuted { get; set; } = false;

        public ChatRole UserRole { get; set; } = ChatRole.Default;

        public DateTime Joined { get; set; } = DateTime.Now;

        public DateTime? MuteEnd { get; set; }

    }
}
