using Messenger.Domains.Dtos;

namespace Messenger.Server.Repositories.UserRepository
{
    public interface IUserRepository
    {

        Task SaveChangesAsync();

        Task<User?> GetUserByGuidAsync(Guid guid);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task CreateUserAsync(User user);

        Task<User?> AuthUserAsync(UserAuthDto user);

        void DeleteUser(User user);
    }
}
