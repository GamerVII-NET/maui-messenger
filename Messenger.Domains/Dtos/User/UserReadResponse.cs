namespace Messenger.Domains.Dtos.User
{
    public class UserReadResponse
    {

        public Guid GlobalGuid { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public DateTime LastOnline { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsBanner { get; set; } = false;

        public bool Confirmed { get; set; } = false;
    }
}
