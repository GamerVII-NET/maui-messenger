using System.ComponentModel.DataAnnotations;

namespace Messenger.Domains.Dtos.User
{
    public class UserAuthDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
