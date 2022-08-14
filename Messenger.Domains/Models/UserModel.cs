using System.ComponentModel.DataAnnotations;

namespace Messenger.Domains.Models
{
    public record UserDto(string UserName, string Password);

    public class UserModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
