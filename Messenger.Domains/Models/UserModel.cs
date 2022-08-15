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

        public string AvatarLink { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
    }
}
