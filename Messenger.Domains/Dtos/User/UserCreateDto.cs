using Messenger.Domains.Models;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Domains.Dtos.User
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public DateTime LastOnline { get; set; } = DateTime.Now;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
