using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messenger.Domains.Models
{
    [Table("Chats")]
    public class Chat
    {
        [Key]
        public Guid GlobalGuid { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public string Photo { get; set; }

        [MaxLength(256)]
        public string About { get; set; }

        public ChatType Type { get; set; }

        public List<ChatUser> Users { get; set; } = new List<ChatUser>();
        public List<Message> Messages { get; set; } = new List<Message>();


    }
}
