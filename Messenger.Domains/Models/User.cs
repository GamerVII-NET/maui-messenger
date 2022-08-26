using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messenger.Domains.Models
{
    public record UserDto(User user);

    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public Guid GlobalGuid { get; set; }

        [Required]
        [MaxLength(32)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        
        public string Patronymic { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        
        public string Photo { get; set; } = string.Empty;

        public DateTime LastOnline { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsBanner { get; set; } = false;

        public bool Confirmed { get; set; } = false;

        public virtual IEnumerable<ChatUser> UserChats { get; set; } = Enumerable.Empty<ChatUser>();

        public override string ToString()
        {
            return $"{GlobalGuid} | {UserName}";
        }

    }
}
